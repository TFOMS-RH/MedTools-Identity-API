using System.Security.Cryptography;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using MedTools.Identity.API.Services;
using MedTools.Identity.API.Common.Options;
using MedTools.Identity.API.Common.Constants;
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
        ConfigureJwtAuthentication(services, configuration);
        ConfigureAuthorizationPolicies(services);

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

    private static void ConfigureJwtAuthentication(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
                ?? throw new InvalidOperationException("Настройки JWT отсутствуют.");

        using var rsa = RSA.Create();

        rsa.ImportFromPem(
            input: File.ReadAllText(
                Path.Combine(
                    AppContext.BaseDirectory,
                    jwtOptions.PublicKeyPemFileName)));


        var publicKey = new RsaSecurityKey(rsa.ExportParameters(false));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidAlgorithms = [SecurityAlgorithms.RsaSha256],
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = true,
                    IssuerSigningKey = publicKey,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

    }

    private static void ConfigureAuthorizationPolicies(
        IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadUsers,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadUsers);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanCreateUsers,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanCreateUsers);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanUpdateUsers,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanUpdateUsers);

                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanUpdateUserStatus,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanUpdateUserStatus);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadUserRoles,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadUserRoles);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanManageUserRoles,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanManageUserRoles);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadUserPermissions,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadUserPermissions);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadOrganizations,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadOrganizations);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanCreateOrganizations,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanCreateOrganizations);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanUpdateOrganizations,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanUpdateOrganizations);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadRoles,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadRoles);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanCreateRoles,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanCreateRoles);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanUpdateRoles,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanUpdateRoles);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanManageRolePermissions,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanManageRolePermissions);
                })
            .AddPolicy(
                MedToolsIdentityPolicyNames.CanReadPermissions,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(
                        "permission",
                        MedToolsIdentityPermissionCodes.CanReadPermissions);
                });
    }
}