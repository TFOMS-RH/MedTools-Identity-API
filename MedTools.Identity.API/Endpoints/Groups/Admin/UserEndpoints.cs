using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.UserManagement;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class UserManagementEndpoints
{
    public static RouteGroupBuilder RegisterUserManagementEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapGet("/users", GetUsersHandler.HandleAsync)
            .WithName("GetUsersList")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        routeGroupBuilder.MapPost("/users", CreateUserHandler.HandleAsync)
            .WithName("CreateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateUsers);

        routeGroupBuilder.MapGet("/users/{id:guid}", GetUserByIdHandler.HandleAsync)
            .WithName("GetUserById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        routeGroupBuilder.MapPatch("/users/{id:guid}", UpdateUserHandler.HandleAsync)
            .WithName("UpdateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUsers);

        routeGroupBuilder.MapPatch("/users/{id:guid}/status", UpdateUserStatusHandler.HandleAsync)
            .WithName("UpdateUserStatus")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUserStatus);

        routeGroupBuilder.MapGet("/users/{id:guid}/roles", GetUserRolesHandler.HandleAsync)
            .WithName("GetUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUserRoles);

        routeGroupBuilder.MapPut("/users/{id:guid}/roles", UpdateUserRolesHandler.HandleAsync)
            .WithName("AddUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageUserRoles);

        routeGroupBuilder.MapGet("/users/{id:guid}/permission-codes", GetUserPermissionCodesHandler.HandleAsync)
            .WithName("GetPermissionCodes")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUserPermissions);

        return routeGroupBuilder;
    }
}