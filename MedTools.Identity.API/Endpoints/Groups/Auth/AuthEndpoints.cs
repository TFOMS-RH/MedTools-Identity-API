using MedTools.Identity.API.Endpoints.Handlers.Auth;

namespace MedTools.Identity.API.Endpoints.Groups.Auth;

public static class AuthEndpoints
{
    public static RouteGroupBuilder RegisterAuthEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/login", LoginHandler.HandleAsync).AllowAnonymous();

        return routeGroupBuilder;
    }
}
