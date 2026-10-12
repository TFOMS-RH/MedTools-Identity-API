using MedTools.Identity.API.Endpoints.Registration;

namespace MedTools.Identity.API.Endpoints.Groups;

public class OrganizationEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/organizations").WithTags("Organizations");

        group.MapGet("/", () => { });
        group.MapPost("/", () => { });
        group.MapPatch("/", () => { });
    }
}
