using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Permissions;

public static class GetPermissionsHandler
{
    public static async Task<IResult> HandleAsync(
        PermissionRepository permissionRepository,
        CancellationToken cancellationToken)
    {
        var permissions = await permissionRepository.GetPermissionsAsync(
            cancellationToken: cancellationToken);

        return Results.Ok(permissions);
    }
}
