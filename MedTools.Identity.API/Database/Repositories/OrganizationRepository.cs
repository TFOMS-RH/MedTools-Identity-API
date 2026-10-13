using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Enums;
using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Repositories;

public sealed class OrganizationRepository(IdentityDbContext dbContext)
{
    public async Task<Guid> CreateOrganizationAsync(
        string code,
        OrganizationType type,
        string? name,
        CancellationToken cancellationToken)
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

    public async Task<bool> IsExistingOrganziationAsync(Guid organizationId)
    {
        return await dbContext
            .Set<OrganizationDbEntity>()
            .AnyAsync(organiation => organiation.Id == organizationId);
    }
}