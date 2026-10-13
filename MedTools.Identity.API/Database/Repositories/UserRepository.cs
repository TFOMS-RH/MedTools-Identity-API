using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Models.Dtos;
using MedTools.Identity.API.Common.Exceptions.Models;

namespace MedTools.Identity.API.Database.Repositories;

public sealed class UserRepository(
    IPasswordHasher<UserDbEntity> passwordHasher,
    IdentityDbContext dbContext)
{
    public async Task<Guid> AddUserAsync(
        string userName,
        string password,
        string firstName,
        string lastName,
        string? middleName,
        string? email,
        string? contactPhone,
        DateOnly? birthDate,
        Guid organizationId,
        CancellationToken cancellationToken)
    {

        try
        {
            var user = new UserDbEntity()
            {
                UserName = userName,
                Email = email,
                BirthDate = birthDate,
                ContactPhone = contactPhone,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                OrganizationId = organizationId,
                IsActive = true
            };

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                password);

            await dbContext
                .Set<UserDbEntity>()
                .AddAsync(user, cancellationToken: cancellationToken);

            await dbContext.SaveChangesAsync(
                cancellationToken: cancellationToken);

            return user.Id;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new DuplicateEntityException("Пользователь с таким именем или Email уже существует.", ex);
        }
    }

    public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Set<UserDbEntity>()
            .Select(user => new UserDto(
                OrganizationId: user.OrganizationId,
                UserName: user.UserName,
                FirstName: user.FirstName,
                LastName: user.LastName,
                MiddleName: user.MiddleName,
                ContactPhone: user.ContactPhone,
                Email: user.Email,
                BitrhDate: user.BirthDate))
            .ToListAsync(cancellationToken);
    }
}