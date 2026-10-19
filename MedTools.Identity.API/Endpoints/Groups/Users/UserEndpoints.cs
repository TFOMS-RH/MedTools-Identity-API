using MedTools.Identity.API.Endpoints.Handlers.Users;

namespace MedTools.Identity.API.Endpoints.Groups.Users;

public static class UserEndpoints
{
    public static RouteGroupBuilder RegisterUserEndpoints(
        this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapGet("/me", GetCurrentUserHandler.HandleAsync)
            .WithName("GetCurrentUser")
            .RequireAuthorization();

        return routeGroupBuilder;
    }
}
