using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Organizations;

public static class GetOrganizationByIdHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        OrganizationRepository organizationRepository,
        CancellationToken cancellationToken)
    {
        var organization = await organizationRepository.GetOrganizationByIdAsync(
            id,
            cancellationToken: cancellationToken);

        return Results.Ok(organization);
    }
}
