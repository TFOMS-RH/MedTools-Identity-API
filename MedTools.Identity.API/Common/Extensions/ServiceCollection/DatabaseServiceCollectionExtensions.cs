using Microsoft.Extensions.Options;
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
        ConfigureDatabaseOptions(services, configuration);
        RegisterDbContext(services);
        RegisterRepositories(services);

        return services;
    }

    private static void ConfigureDatabaseOptions(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DatabaseOption>()
            .Bind(configuration.GetSection(DatabaseOption.SectionName))
            .Validate(options =>
                !string.IsNullOrWhiteSpace(options.ConnectionString) &&
                options.CommandTimeout > 0)
            .ValidateOnStart();
    }

    private static void RegisterDbContext(
        IServiceCollection services)
    {

        services.AddDbContext<IdentityDbContext>((sp, dbContextOptionBuilder) =>
        {
            var databaseOption = sp.GetRequiredService<IOptions<DatabaseOption>>().Value;

            dbContextOptionBuilder.UseSqlServer(
                databaseOption.ConnectionString,
                options =>
                {
                    options.CommandTimeout(databaseOption.CommandTimeout);
                });
        });
    }

    private static void RegisterRepositories(
        IServiceCollection services)
    {
        services.AddScoped<OrganizationRepository>();
        services.AddScoped<PermissionRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<RoleRepository>();
    }
}
