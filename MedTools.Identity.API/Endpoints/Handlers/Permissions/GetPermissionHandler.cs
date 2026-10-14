using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Permissions;

public static class GetPermissionHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        PermissionRepository permissionRepository,
        CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetPermissionByIdAsync(
            permissionId: id,
            cancellationToken: cancellationToken);

        return Results.Ok(permission);
    }
}
