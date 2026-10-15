using MedTools.Identity.API.Common.Models.Requests;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class UpdateUserRolesHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateUserRolesRequest request,
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        await userRepository.UpdateUserRolesAsync(
            userId: id,
            roleIds: request.RoleIds,
            cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}