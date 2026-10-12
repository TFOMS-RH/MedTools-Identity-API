namespace MedTools.Identity.API.Endpoints.Registration;

public static class EndpointsExtenstion
{
    public static IEndpointRouteBuilder Initialize<T>(
        this IEndpointRouteBuilder endpointRouteBuilder)
        where T : IEndpoint, new()
    {
        var endpointsProvider = new T();

        endpointsProvider.Initialize(endpointRouteBuilder);

        return endpointRouteBuilder;
    }
}
