using MedTools.Identity.API.Services;
using MedTools.Identity.API.Common.Models.Requests;

namespace MedTools.Identity.API.Endpoints.Handlers.Auth;

public static class LoginHandler
{
    public static async Task<IResult> HandleAsync(
        LoginRequest request,
        AuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var result = await authenticationService.AuthenticateAsync(
            userName: request.UserName,
            password: request.Password,
            cancellationToken: cancellationToken);

        return TypedResults.Ok(result);
    }
}
