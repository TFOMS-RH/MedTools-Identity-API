using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Roles;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class RoleEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/roles").WithTags("Roles");

        group.MapPost("/", CreateRoleHandler.HandleAsync);
        group.MapGet("/", GetRolesHandler.HandleAsync);
        group.MapGet("/{id:guid}", GetRoleByIdHandler.HandleAsync).WithName("GetRoleById");
        group.MapPatch("/{id}", () => { });
        group.MapDelete("/{id}", () => { });
    }
}
