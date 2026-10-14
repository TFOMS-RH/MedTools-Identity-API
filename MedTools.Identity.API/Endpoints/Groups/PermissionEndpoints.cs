using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Permissions;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class PermissionEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/permissions").WithTags("Permissions");

        group.MapPost("/", CreatePermissionHandler.HandleAsync);
        group.MapGet("/", GetPermissionsHandler.HandleAsync);
        group.MapGet("/{id:guid}", GetPermissionHandler.HandleAsync).WithName("GetPermissionById");
    }
}
