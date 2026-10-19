using MedTools.Identity.API.Endpoints.Groups.Auth;
using MedTools.Identity.API.Endpoints.Groups.Admin;
using MedTools.Identity.API.Endpoints.Groups.Users;

namespace MedTools.Identity.API.Endpoints.Groups;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder RegisterIdentityEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var adminGroup = endpointRouteBuilder.MapGroup("/admin").RequireAuthorization();
        adminGroup.RegisterOrganizationEndpoints();
        adminGroup.RegisterPermissionEndpoints();
        adminGroup.RegisterRoleEndpoints();
        adminGroup.RegisterUserManagementEndpoints();

        var usersGroup = endpointRouteBuilder.MapGroup("/users").RequireAuthorization();
        usersGroup.RegisterUserEndpoints();

        var authGroup = endpointRouteBuilder.MapGroup("/auth");
        authGroup.RegisterAuthEndpoints();


        return endpointRouteBuilder;
    }
}
