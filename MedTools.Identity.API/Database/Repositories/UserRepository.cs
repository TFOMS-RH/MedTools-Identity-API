using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Models.Dtos;
using MedTools.Identity.API.Common.Models.Params;
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
            throw new DuplicateEntityException("Пользователь с таким именем уже существует.", ex);
        }
    }

    public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync(
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Set<UserDbEntity>()
            .Select(user => new UserDto(
                Id: user.Id,
                OrganizationId: user.OrganizationId,
                UserName: user.UserName,
                FirstName: user.FirstName,
                LastName: user.LastName,
                MiddleName: user.MiddleName,
                ContactPhone: user.ContactPhone,
                Email: user.Email,
                BirthDate: user.BirthDate))
            .ToListAsync(cancellationToken);
    }

    public async Task<UserDto> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var dbUser = await dbContext
            .Set<UserDbEntity>()
            .FindAsync(
                [userId],
                cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException($"Пользователь с Id {userId} не был найден.");

        return new UserDto(
            Id: dbUser.Id,
            OrganizationId: dbUser.OrganizationId,
            UserName: dbUser.UserName,
            FirstName: dbUser.FirstName,
            LastName: dbUser.LastName,
            MiddleName: dbUser.MiddleName,
            ContactPhone: dbUser.ContactPhone,
            Email: dbUser.Email,
            BirthDate: dbUser.BirthDate);
    }

    public async Task UpdateUserAsync(
        Guid userId,
        UpdateUserParams updateParams,
        CancellationToken cancellationToken)
    {
        var user = await dbContext
            .Set<UserDbEntity>()
            .FindAsync(keyValues: [userId], cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException($"Пользователь с Id {userId} не найден.");

        if (updateParams.FirstName.IsSpecified)
        {
            user.FirstName = updateParams.FirstName.Value!;
        }

        if (updateParams.LastName.IsSpecified)
        {
            user.LastName = updateParams.LastName.Value!;
        }

        if (updateParams.MiddleName.IsSpecified)
        {
            user.MiddleName = updateParams.MiddleName.Value;
        }

        if (updateParams.ContactPhone.IsSpecified)
        {
            user.ContactPhone = updateParams.ContactPhone.Value;
        }

        if (updateParams.Email.IsSpecified)
        {
            user.Email = updateParams.Email.Value;
        }

        if (updateParams.BirthDate.IsSpecified)
        {
            user.BirthDate = updateParams.BirthDate.Value;
        }

        await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }

    public async Task UpdateUserStatusAsync(
        Guid userId,
        bool isActive,
        CancellationToken cancellationToken)
    {
        var user = await dbContext
            .Set<UserDbEntity>()
            .FindAsync([userId], cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException($"Пользователь с Id {userId} не найден.");

        user.IsActive = isActive;

        await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}