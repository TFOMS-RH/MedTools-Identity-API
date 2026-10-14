using MedTools.Identity.API.Common.Enums;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Organizations;

public static class GetOrganizationsHandler
{
    public static async Task<IResult> HandleAsync(
        OrganizationType? type,
        OrganizationRepository organizationRepository,
        CancellationToken cancellationToken)
    {
        var organizations = await organizationRepository.GetOrganizationsAsync(
            type,
            cancellationToken);

        return Results.Ok(organizations);
    }
}
