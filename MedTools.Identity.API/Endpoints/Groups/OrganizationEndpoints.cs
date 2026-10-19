using MedTools.Identity.API.Common.Constants;
using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Endpoints.Handlers.Organizations;

namespace MedTools.Identity.API.Endpoints.Groups;

public class OrganizationEndpoints : IEndpoint
{
    public void Initialize(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder
            .MapGroup("/organizations")
            .WithTags("Organizations")
            .RequireAuthorization();


        group.MapPost("/", CreateOrganizationHandler.HandleAsync)
            .WithName("CreateOrganziation")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanCreateOrganizations);

        group.MapGet("/", GetOrganizationsHandler.HandleAsync)
            .WithName("GetOrganizations")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);

        group.MapGet("/{id:guid}", GetOrganizationByIdHandler.HandleAsync)
            .WithName("GetOrganizationById")
            .RequireAuthorization(MedToolsIdentityPolicyNames.CanReadOrganizations);
    }
}
