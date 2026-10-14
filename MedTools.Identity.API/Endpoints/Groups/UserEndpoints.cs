using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Users;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class UserEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("users").WithTags("Users");

        group.MapPost("/", CreateUserHandler.HandleAsync);
        group.MapGet("/", GetUsersHandler.HandleAsync);
        group.MapGet("/{id:guid}", GetUserByIdHandler.HandleAsync).WithName("GetUserById");
        group.MapPatch("/{id:guid}", UpdateUserHandler.HandleAsync);
        group.MapPatch("/{id}/status", () => { });

    }
}
