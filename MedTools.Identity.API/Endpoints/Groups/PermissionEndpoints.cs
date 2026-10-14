using MedTools.Identity.API.Endpoints.Handlers.Permissions;
using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class PermissionEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/permissions").WithTags("Persmissions");

        group.MapPost("/", CreatePermissionHandler.HandleAsync);
        group.MapGet("/{id:guid}", () => { }).WithName("GetPermissionById");
    }
}
