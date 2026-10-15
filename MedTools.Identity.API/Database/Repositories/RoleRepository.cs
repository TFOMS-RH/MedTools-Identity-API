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

    public async Task<IReadOnlyCollection<RoleDto>> GetRolesAsync(
        CancellationToken cancellationToken)
    {
        var roles = await dbContext
            .Set<RoleDbEntity>()
            .OrderBy(x => x.Name)
            .Select(x => new RoleDto(
                Id: x.Id,
                Name: x.Name))
            .ToListAsync(cancellationToken: cancellationToken);

        return roles;
    }

    public async Task UpdateRolePermissionsAsync(
        Guid roleId,
        List<Guid> permissionIds,
        CancellationToken cancellationToken)
    {
        var role = await dbContext
            .Set<RoleDbEntity>()
            .Where(x => x.Id == roleId)
            .Include(x => x.Permissions)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException($"Роли с Id {roleId} не найдено.");

        var requestedPermissionIds = permissionIds.ToHashSet();

        var filterPermissions = await dbContext
            .Set<PermissionDbEntity>()
            .Where(x => requestedPermissionIds.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        if(requestedPermissionIds.Count != filterPermissions.Count)
        {
            throw new EntityNotFoundException(
                "Одно или более из переданных разрешений не существует в системе.");
        }

        var currentPermissionsIds = role.Permissions
            .Select(x => x.Id)
            .ToHashSet();

        var idsToRemove = currentPermissionsIds.Except(requestedPermissionIds);
        var idsToAdd = requestedPermissionIds.Except(currentPermissionsIds);

        foreach(var permission in role.Permissions
            .Where(x => idsToRemove.Contains(x.Id))
            .ToList())
        {
            role.Permissions.Remove(permission);
        }

        foreach(var permission in filterPermissions
            .Where(x => idsToAdd.Contains(x.Id))
            .ToList())
        {
            role.Permissions.Add(permission);
        }

        await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

    }
}