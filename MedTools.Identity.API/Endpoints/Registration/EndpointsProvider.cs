using MedTools.Identity.API.Endpoints.Groups;

namespace MedTools.Identity.API.Endpoints.Registration;

public static class EndpointsProvider
{
    public static void RegisterApplicationEmdpoints(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.Initialize<AuthEndpoints>();
    }
}
