using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Handlers.Admin.UserManagement;

namespace MedTools.Identity.API.Endpoints.Groups.Admin;

public static class UserManagementEndpoints
{
    public static RouteGroupBuilder RegisterUserManagementEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {

        var group = routeGroupBuilder
            .MapGroup("/users")
            .WithTags("Users");

        group.MapGet(
                pattern: "/",
                handler: GetUsersHandler.HandleAsync)
            .WithName("GetUsersList")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        group.MapPost(
                pattern: "/",
                handler: CreateUserHandler.HandleAsync)
            .WithName("CreateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateUsers);

        group.MapGet(
                pattern: "/{id:guid}",
                handler: GetUserByIdHandler.HandleAsync)
            .WithName("GetUserById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        group.MapPatch(
                pattern: "/{id:guid}",
                handler: UpdateUserHandler.HandleAsync)
            .WithName("UpdateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUsers);

        group.MapPatch(
                pattern: "/{id:guid}/status", 
                handler: UpdateUserStatusHandler.HandleAsync)
            .WithName("UpdateUserStatus")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUserStatus);

        group.MapGet(
                pattern: "/{id:guid}/roles", 
                handler: GetUserRolesHandler.HandleAsync)
            .WithName("GetUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUserRoles);

        group.MapPut(
                pattern: "/{id:guid}/roles", 
                handler: UpdateUserRolesHandler.HandleAsync)
            .WithName("AddUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageUserRoles);

        group.MapGet(
                pattern: "/{id:guid}/permission-codes", 
                handler: GetUserPermissionCodesHandler.HandleAsync)
            .WithName("GetPermissionCodes")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUserPermissions);

        return group;
    }
}