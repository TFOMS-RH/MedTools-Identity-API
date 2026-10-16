using Microsoft.AspNetCore.Identity;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Common.Extensions.ServiceCollection;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddMedToolsAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<UserDbEntity>, PasswordHasher<UserDbEntity>>();
        

        return services;
    }

}
