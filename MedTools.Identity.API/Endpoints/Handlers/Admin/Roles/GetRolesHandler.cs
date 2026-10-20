using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

public static class GetRolesHandler
{
    public static async Task<IResult> HandleAsync(
        RoleRepository roleRepository,
        CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetRolesAsync(
            cancellationToken: cancellationToken);

        return Results.Ok(roles);
    }
}
