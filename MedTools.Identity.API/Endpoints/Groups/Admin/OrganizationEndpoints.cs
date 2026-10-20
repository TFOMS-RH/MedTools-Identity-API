using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Organizations;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class OrganizationEndpoints
{
    public static RouteGroupBuilder RegisterOrganizationEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder
            .MapGroup("/organizations")
            .WithTags("Organizations");

        group.MapPost(
                pattern: "/",
                handler: CreateOrganizationHandler.HandleAsync)
            .WithName("CreateOrganziation")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateOrganizations);

        group.MapGet(
                pattern: "/",
                handler: GetOrganizationsHandler.HandleAsync)
            .WithName("GetOrganizations")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);

        group.MapGet(
                pattern: "/{id:guid}",
                handler: GetOrganizationByIdHandler.HandleAsync)
            .WithName("GetOrganizationById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);

        return group;
    }
}
