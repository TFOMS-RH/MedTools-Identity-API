using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Common.Exceptions.Models;
using MedTools.Identity.API.Database.Entities;

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
}