using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Permissions;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class PermissionEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder
            .MapGroup("/permissions")
            .WithTags("Permissions")
            .RequireAuthorization();


        group.MapPost("/", CreatePermissionHandler.HandleAsync)
            .WithName("CreatePermission")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreatePermissions);

        group.MapGet("/", GetPermissionsHandler.HandleAsync)
            .WithName("GetPermissions")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);

        group.MapGet("/{id:guid}", GetPermissionByIdHandler.HandleAsync)
            .WithName("GetPermissionById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadPermissions);
    }
}
