using MedTools.Identity.API.Endpoints.Handlers.Auth;
using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public sealed class AuthEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("auth").WithTags("Authorization");

        group.MapPost("/login", LoginHandler.HandleAsync);
        group.MapPost("/logout", () => { });
        group.MapPost("/refresh", () => { });

    }
}
