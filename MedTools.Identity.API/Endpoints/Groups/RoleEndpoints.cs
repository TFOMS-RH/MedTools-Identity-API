using MedTools.Identity.API.Endpoints.Handlers.Roles;
using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class RoleEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/roles").WithTags("Roles");

        group.MapPost("/", CreateRoleHandler.HandleAsync);
        group.MapGet("/{id:guid}", GetRoleByIdHandler.HandleAsync).WithName("GetRoleById");
        group.MapPatch("/{id}", () => { });
        group.MapDelete("/{id}", () => { });
    }
}
