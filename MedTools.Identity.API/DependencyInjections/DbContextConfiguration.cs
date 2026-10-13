using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database;
using MedTools.Identity.API.Database.Repositories;
using MedTools.Identity.API.Common.Options;

namespace MedTools.Identity.API.DependencyInjections;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        DatabaseOption databaseOption = new();

        configuration.GetSection(DatabaseOption.SectionName)
            .Bind(databaseOption);

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(
                databaseOption.ConnectionString,
                options =>
                {
                    options.CommandTimeout(databaseOption.CommandTimeout);
                });
        });

        return services;
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<OrganizationRepository>();

        return services;
    }
}
