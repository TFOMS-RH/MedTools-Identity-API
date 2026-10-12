using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database;
using MedTools.Identity.API.Options;

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
}
