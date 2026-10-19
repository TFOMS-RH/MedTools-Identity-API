using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.Permissions;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class PermissionEndpoints
{
    public static RouteGroupBuilder RegisterPermissionEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/permissions", CreatePermissionHandler.HandleAsync)
            .WithName("CreatePermission")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreatePermissions);

        routeGroupBuilder.MapGet("/permissions", GetPermissionsHandler.HandleAsync)
            .WithName("GetPermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);

        routeGroupBuilder.MapGet("/permissions/{id:guid}", GetPermissionByIdHandler.HandleAsync)
            .WithName("GetPermissionById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);

        return routeGroupBuilder;
    }
}
