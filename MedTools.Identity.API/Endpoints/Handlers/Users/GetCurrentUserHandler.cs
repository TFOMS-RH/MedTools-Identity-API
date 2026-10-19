using MedTools.Identity.API.Database.Repositories;
using System.Security.Claims;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class GetCurrentUserHandler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal claimsPrincipal,
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(
            userIdClaim,
            out var userId))
        {
            return Results.Unauthorized();
        }

        var user = await userRepository.GetUserByIdAsync(
            userId: userId,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(user);
    }
}
