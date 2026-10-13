using Microsoft.AspNetCore.Identity;

using MedTools.Identity.API.Database.Entities;

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
}
