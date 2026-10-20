using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.UserManagement;

public static class GetUserRolesHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        RoleRepository roleRepository,
        CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetRolesByUserIdAsync(
            userId: id,
            cancellationToken: cancellationToken);

        return Results.Ok(roles);
    }
}
