using MedTools.Identity.API.Endpoints.Handlers.Users;
using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class UserEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("users").WithTags("Users");

        group.MapPost("/", CreateUserHandler.HandleAsync);
        group.MapGet("/", () => { });
        group.MapGet("/{id:guid}", () => { }).WithName("GetUserById");
        group.MapPatch("/{id}", () => { });
        group.MapPatch("/{id}/status", () => { });
        group.MapPost("/{id}/reset-password", () => { });

    }
}
