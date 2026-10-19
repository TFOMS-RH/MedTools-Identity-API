using MedTools.Identity.API.Common.Models.Requests;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.Roles;

public static class UpdateRolePermissionsHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateRolePermissionsRequest request,
        RoleRepository roleRepository,
        CancellationToken cancellationToken)
    {
        await roleRepository.UpdateRolePermissionsAsync(
            roleId: id,
            permissionIds: request.PermissionIds,
            cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
