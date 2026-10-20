using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

public static class GetRolePermissionsHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        PermissionRepository permissionRepository,
        CancellationToken cancellationToken)
    {
        var permissions = await permissionRepository.GetPermissionsByRoleId(
            roleId: id,
            cancellationToken: cancellationToken);

        return Results.Ok(permissions);
    }
}
