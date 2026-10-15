using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class GetUserPermissionCodesHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        PermissionRepository permissionRepository,
        CancellationToken cancellationToken)
    {
        var permissionCodes = await permissionRepository.GetUserPermissionCodesAsync(
            userId: id,
            cancellationToken: cancellationToken);

        return Results.Ok(permissionCodes);
    }
}
