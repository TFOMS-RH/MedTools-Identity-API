using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Endpoints.Handlers.Users;

public static class GetUserByIdHandler
{
    public async static Task<IResult> HandleAsync(
        Guid id,
        UserRepository userRepository,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(id, cancellationToken);

        return Results.Ok(user);
    }
}
