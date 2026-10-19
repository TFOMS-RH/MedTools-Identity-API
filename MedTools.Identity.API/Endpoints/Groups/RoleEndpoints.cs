using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Roles;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class RoleEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder
            .MapGroup("/roles")
            .WithTags("Roles")
            .RequireAuthorization();


        group.MapPost("/", CreateRoleHandler.HandleAsync)
            .WithName("CreateRole")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateRoles);

        group.MapGet("/", GetRolesHandler.HandleAsync)
            .WithName("GetRoles")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        group.MapGet("/{id:guid}", GetRoleByIdHandler.HandleAsync)
            .WithName("GetRoleById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRoles);

        group.MapGet("/{id:guid}/permissions", GetRolePermissionsHandler.HandleAsync)
            .WithName("GetRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadRolePermissions);

        group.MapPut("/{id:guid}/permissions", UpdateRolePermissionsHandler.HandleAsync)
            .WithName("UpdateRolePermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanManageRolePermissions);
    }
}
