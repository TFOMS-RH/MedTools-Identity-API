using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class RoleEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/roles").WithTags("Roles");

        group.MapGet("/", () => { });
        group.MapPost("/", () => { });
        group.MapPatch("/{id}", () => { });
        group.MapDelete("/{id}", () => { });
    }
}
