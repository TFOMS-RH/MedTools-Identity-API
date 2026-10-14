using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Models.Dtos;
using MedTools.Identity.API.Common.Exceptions.Models;

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

    public async Task<RoleDto> GetRoleByIdAsync(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        var role = await dbContext
            .Set<RoleDbEntity>()
            .Where(x => x.Id == roleId)
            .Select(x => new RoleDto(
                Id: x.Id,
                Name: x.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException($"Роли с Id {roleId} не найдено.");

        return role;
    }
}