using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Permissions;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class PermissionEndpoints
{
    public static RouteGroupBuilder RegisterPermissionEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder
            .MapGroup("/permissions")
            .WithTags("Permissions");

        group.MapPost(
                pattern: "/",
                handler: CreatePermissionHandler.HandleAsync)
            .WithName("CreatePermission")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreatePermissions);

        group.MapGet(
                pattern: "/",
                handler: GetPermissionsHandler.HandleAsync)
            .WithName("GetPermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);

        group.MapGet(
                pattern: "/{id:guid}",
                handler: GetPermissionByIdHandler.HandleAsync)
            .WithName("GetPermissionById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);

        return group;

    }
}
