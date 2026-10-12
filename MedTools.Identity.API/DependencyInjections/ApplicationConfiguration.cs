using Microsoft.AspNetCore.Identity;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.DependencyInjections;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddMedToolsIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<UserDbEntity>, PasswordHasher<UserDbEntity>>();

        return services;
    }
}
