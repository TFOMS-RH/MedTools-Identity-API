using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Organizations;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class OrganizationEndpoints
{
    public static RouteGroupBuilder RegisterOrganizationEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/organizations", CreateOrganizationHandler.HandleAsync)
            .WithName("CreateOrganziation")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateOrganizations);

        routeGroupBuilder.MapGet("/organizations", GetOrganizationsHandler.HandleAsync)
            .WithName("GetOrganizations")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);

        routeGroupBuilder.MapGet("/organizations/{id:guid}", GetOrganizationByIdHandler.HandleAsync)
            .WithName("GetOrganizationById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);

        return routeGroupBuilder;
    }
}
