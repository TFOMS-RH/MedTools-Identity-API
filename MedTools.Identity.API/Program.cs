using Serilog;

using MedTools.Identity.API.DependencyInjections;
using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Common.Exceptions.Handlers;

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
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Host.UseSerilog();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8007);
        });

        var app = builder.Build();

        app.UseExceptionHandler();

        EndpointsProvider.RegisterApplicationEmdpoints(app.MapGroup("/api"));

        await app.RunAsync();
    }
}