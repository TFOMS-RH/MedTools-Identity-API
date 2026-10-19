using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class RoleEndpoints
{
    public static RouteGroupBuilder RegisterRoleEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/roles", CreateRoleHandler.HandleAsync)
            .WithName("CreateRole")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateRoles);

        routeGroupBuilder.MapGet("/roles", GetRolesHandler.HandleAsync)
            .WithName("GetRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        routeGroupBuilder.MapGet("/roles/{id:guid}", GetRoleByIdHandler.HandleAsync)
            .WithName("GetRoleById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        routeGroupBuilder.MapGet("/roles/{id:guid}/permissions", GetRolePermissionsHandler.HandleAsync)
            .WithName("GetRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRolePermissions);

        routeGroupBuilder.MapPut("/roles/{id:guid}/permissions", UpdateRolePermissionsHandler.HandleAsync)
            .WithName("UpdateRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageRolePermissions);

        return routeGroupBuilder;

    }
}
