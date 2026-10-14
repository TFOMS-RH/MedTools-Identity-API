using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Models.Dtos;
using MedTools.Identity.API.Common.Exceptions.Models;

namespace MedTools.Identity.API.Database.Repositories;

public sealed class PermissionRepository(
    IdentityDbContext dbContext)
{
    public async Task<Guid> AddPermissionAsync(
        string code,
        string name,
        string? description,
        CancellationToken cancellationToken)
    {
        try
        {
            var permission = new PermissionDbEntity()
            {
                Code = code,
                Description = description,
                Name = name
            };

            await dbContext.Set<PermissionDbEntity>().AddAsync(
                permission,
                cancellationToken: cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return permission.Id;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new DuplicateEntityException(
                "Разрешение с данным code уже существует.", ex);
        }
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(
        Guid permissionId,
        CancellationToken cancellationToken)
    {
        var permission = await dbContext
            .Set<PermissionDbEntity>()
            .AsNoTracking()
            .Where(x => x.Id == permissionId)
            .Select(x => new PermissionDto(
                Id: x.Id,
                Code: x.Code,
                Name: x.Name,
                Description: x.Description))
            .FirstOrDefaultAsync(
                cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException($"Разрешения с Id {permissionId} не найдено.");

        return permission;

    }

    public async Task<IReadOnlyCollection<PermissionDto>> GetPermissionsAsync(
        CancellationToken cancellationToken)
    {
        var permissions = await dbContext
            .Set<PermissionDbEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .Select(x => new PermissionDto(
                Id: x.Id,
                Code: x.Code,
                Name: x.Name,
                Description: x.Description))
            .ToListAsync(cancellationToken: cancellationToken);

        return permissions;
    }
}