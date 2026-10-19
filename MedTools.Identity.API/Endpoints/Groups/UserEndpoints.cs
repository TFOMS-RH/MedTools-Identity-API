using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Users;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class UserEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder
            .MapGroup("users")
            .WithTags("Users")
            .RequireAuthorization();

        group.MapGet("/", GetUsersHandler.HandleAsync)
            .WithName("GetUsersList")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        group.MapPost("/", CreateUserHandler.HandleAsync)
            .WithName("CreateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateUsers);

        group.MapGet("/{id:guid}", GetUserByIdHandler.HandleAsync)
            .WithName("GetUserById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);

        group.MapPatch("/{id:guid}", UpdateUserHandler.HandleAsync)
            .WithName("UpdateUser")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUsers);
        
        group.MapPatch("/{id:guid}/status", UpdateUserStatusHandler.HandleAsync)
            .WithName("UpdateUserStatus")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanUpdateUserStatus);
        
        group.MapGet("/{id:guid}/roles", GetUserRolesHandler.HandleAsync)
            .WithName("GetUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUserRoles);

        group.MapPut("/{id:guid}/roles", UpdateUserRolesHandler.HandleAsync)
            .WithName("AddUserRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageUserRoles);

        group.MapGet("/{id:guid}/permission-codes", GetUserPermissionCodesHandler.HandleAsync)
            .WithName("GetPermissionCodes")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadUsers);
    }
}