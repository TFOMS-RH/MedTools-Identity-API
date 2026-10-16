using Microsoft.EntityFrameworkCore;

using MedTools.Identity.API.Database;
using MedTools.Identity.API.Common.Options;
using MedTools.Identity.API.Database.Repositories;

namespace MedTools.Identity.API.Common.Extensions.ServiceCollection;

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        DatabaseOption databaseOption = new();

        configuration.GetSection(DatabaseOption.SectionName).Bind(databaseOption);

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(
                databaseOption.ConnectionString,
                options =>
                {
                    options.CommandTimeout(databaseOption.CommandTimeout);
                });
        });

        services.AddScoped<OrganizationRepository>();
        services.AddScoped<PermissionRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<RoleRepository>();

        return services;
    } 
}
