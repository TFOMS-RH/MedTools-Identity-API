using MedTools.Identity.API.Common.Models.Requests;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class UpdateUserStatusHandler
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateUserStatusRequest request,
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        await userRepository.UpdateUserStatusAsync(
            userId: id,
            isActive: request.IsActive,
            cancellationToken: cancellationToken);

        return Results.NoContent();
    }
}
