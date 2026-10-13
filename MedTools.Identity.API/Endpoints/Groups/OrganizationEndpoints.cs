using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Organizations;

namespace MedTools.Identity.API.Endpoints.Groups;

public class OrganizationEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/organizations").WithTags("Organizations");

        group.MapPost("/", CreateOrganizationHandler.HandleAsync);
        group.MapGet("/", () => { }).WithName("GetOrganizationById");
        group.MapPatch("/", () => { });
    }
}
