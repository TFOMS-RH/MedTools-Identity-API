using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Common.Enums;
using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Common.Models.Dtos;
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

    public async Task<IReadOnlyCollection<OrganizationDto>> GetOrganizationsAsync(
        OrganizationType? type,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Set<OrganizationDbEntity>().AsQueryable();

        if (type is not null)
        {
            query = query.Where(x => x.Type == type);
        }

        return await query
            .Select(x => new OrganizationDto(
                Id: x.Id,
                Type: x.Type,
                Code: x.Code,
                Name: x.Name))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<OrganizationDto> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        var organization = await dbContext
            .Set<OrganizationDbEntity>()
            .FindAsync([organizationId], cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException($"Организации с Id {organizationId} не найдено.");

        return new OrganizationDto(
            Id: organization.Id,
            Type: organization.Type,
            Code: organization.Code,
            Name: organization.Name);
    }
}