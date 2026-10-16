using System.Security.Cryptography;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MedTools.Identity.API.Services;
using MedTools.Identity.API.Common.Options;
using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Common.Extensions.ServiceCollection;

public static class IdentityServiceCollectionExtensions
{

    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddMedToolsAuthenticationServices(services);
        AddJwtConfigurationServices(services, configuration);

        return services;
    }

    private static void AddMedToolsAuthenticationServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<UserDbEntity>, PasswordHasher<UserDbEntity>>();
    }

    private static void AddJwtConfigurationServices(
        IServiceCollection services, 
        IConfiguration configuration)
    {

        services.AddOptions<JwtOptions>()
           .Bind(configuration.GetSection(JwtOptions.SectionName))
           .Validate(options =>
               !string.IsNullOrWhiteSpace(options.Issuer) &&
               !string.IsNullOrWhiteSpace(options.Audience) &&
               !string.IsNullOrWhiteSpace(options.PrivateKeyPemFileName) &&
               options.AccessTokenExpirationTimeInMinutes > 0)
           .ValidateOnStart();

        services.AddSingleton<RSA>(sp =>
        {

            var jwtOptions = sp.GetRequiredService<IOptions<JwtOptions>>().Value;

            var privateKeyPath = Path.Combine(
                AppContext.BaseDirectory, 
                jwtOptions.PrivateKeyPemFileName);

            var privateKey = File.ReadAllText(privateKeyPath);

            var rsa = RSA.Create();

            try
            {
                rsa.ImportFromPem(privateKey);
                rsa.ExportParameters(true);

                return rsa;
            }
            catch
            {
                rsa.Dispose();
                throw;
            }

        });
        services.AddSingleton<JwtTokenGenerationService>();
        services.AddSingleton<SigningCredentials>((sp) =>
        {
            var rsa = sp.GetRequiredService<RSA>();
            var securityKey = new RsaSecurityKey(rsa);

            return new SigningCredentials(
                key: securityKey,
                algorithm: SecurityAlgorithms.RsaSha256);

        });

        services.AddScoped<AuthenticationService>();
    }
}