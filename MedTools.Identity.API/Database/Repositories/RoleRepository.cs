using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Repositories;

public sealed class RoleRepository(IdentityDbContext dbContext)
{
    public async Task<Guid> AddRoleAsync(
        string name,
        CancellationToken cancellationToken)
    {
        var role = new RoleDbEntity()
        {
            Name = name,
        };

        await dbContext
            .Set<RoleDbEntity>()
            .AddAsync(
                role,
                cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

        return role.Id;
    }
}