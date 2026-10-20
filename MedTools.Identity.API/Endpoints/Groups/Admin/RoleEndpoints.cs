using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class RoleEndpoints
{
    public static RouteGroupBuilder RegisterRoleEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder
            .MapGroup("/roles")
            .WithTags("Roles");

        group.MapPost(
                pattern: "/",
                handler: CreateRoleHandler.HandleAsync)
            .WithName("CreateRole")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateRoles);

        group.MapGet(
                pattern: "/",
                handler: GetRolesHandler.HandleAsync)
            .WithName("GetRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        group.MapGet(
                pattern: "/{id:guid}",
                handler: GetRoleByIdHandler.HandleAsync)
            .WithName("GetRoleById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        group.MapGet(
                pattern: "/{id:guid}/permissions",
                handler: GetRolePermissionsHandler.HandleAsync)
            .WithName("GetRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRolePermissions);

        group.MapPut(
                pattern: "/{id:guid}/permissions",
                handler: UpdateRolePermissionsHandler.HandleAsync)
            .WithName("UpdateRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageRolePermissions);

        return group;

    }
}
