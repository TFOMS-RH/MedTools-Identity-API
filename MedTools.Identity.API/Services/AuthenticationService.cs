using Microsoft.AspNetCore.Identity;

using MedTools.Identity.API.Database.Entities;
using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Models.Results;
using MedTools.Identity.API.Common.Exceptions.Models;

namespace MedTools.Identity.API.Services;

public sealed class AuthenticationService(
    UserRepository userRepository,
    PermissionRepository permissionRepository,
    JwtTokenGenerationService jwtTokenGenerationService,
    IPasswordHasher<UserDbEntity> passwordHasher)
{
    public async Task<AuthenticationResult> AuthenticateAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetUserByUserNameAsync(
            userName: userName,
            cancellationToken: cancellationToken)
                ?? throw new MedToolsAuthenticationFailedException();

        if (!user.IsActive) throw new MedToolsAuthenticationFailedException();

        var passwordVerifyResult = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password);

        if (passwordVerifyResult == PasswordVerificationResult.Failed) throw new MedToolsAuthenticationFailedException();

        var userPermissions = await permissionRepository.GetUserPermissionCodesAsync(
            userId: user.Id,
            cancellationToken: cancellationToken);

        var accessToken = jwtTokenGenerationService.GenerateAccessToken(
            user,
            userPermissions);

        return new AuthenticationResult(
            AccessToken: accessToken,
            TokenType: "Bearer");
    }
}