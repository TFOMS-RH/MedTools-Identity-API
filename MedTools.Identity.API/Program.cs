using Serilog;

using MedTools.Identity.API.DependencyInjections;
using MedTools.Identity.API.Endpoints.Registration;
using System.Text.Json.Serialization;

namespace MedTools.Identity.API;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "medtools_logs/log.txt"),
                rollingInterval: RollingInterval.Hour,
                rollOnFileSizeLimit: true)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder();

        builder.Services.AddDbContextConfiguration(builder.Configuration);
        builder.Services.AddApplicationRepositories();
        builder.Services.AddMedToolsIdentityServices();
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        

        builder.Host.UseSerilog();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8007);
        });

        var app = builder.Build();

        EndpointsProvider.RegisterApplicationEmdpoints(app.MapGroup("/api"));

        await app.RunAsync();
    }
}