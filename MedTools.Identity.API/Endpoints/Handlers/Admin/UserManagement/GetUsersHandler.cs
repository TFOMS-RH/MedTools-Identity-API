using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Admin.UserManagement;

public static class GetUsersHandler
{
    public static async Task<IResult> HandleAsync(
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(cancellationToken: cancellationToken);

        return Results.Ok(users);
    }
}
