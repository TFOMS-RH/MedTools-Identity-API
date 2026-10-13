using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Common.Enums;
using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Exceptions.Models;

namespace MedTools.Identity.API.Database.Repositories;

public sealed class OrganizationRepository(IdentityDbContext dbContext)
{
    public async Task<Guid> CreateOrganizationAsync(
        string code,
        OrganizationType type,
        string? name,
        CancellationToken cancellationToken)
    {
        try
        {
            var organization = new OrganizationDbEntity()
            {
                Code = code,
                Type = type,
                Name = name
            };

            await dbContext
                .Set<OrganizationDbEntity>()
                .AddAsync(organization, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return organization.Id;
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new DuplicateEntityException("Организация с данным кодом уже существует.", ex);
        }
    }

    public async Task<bool> IsExistingOrganziationAsync(Guid organizationId)
    {
        return await dbContext
            .Set<OrganizationDbEntity>()
            .AnyAsync(organiation => organiation.Id == organizationId);
    }
}