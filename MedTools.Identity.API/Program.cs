using Serilog;

using MedTools.Identity.API.Endpoints.Registration;
using MedTools.Identity.API.Common.Exceptions.Handlers;
using MedTools.Identity.API.Common.Extensions.ServiceCollection;

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

        builder.Services.AddDatabaseServices(builder.Configuration);
        builder.Services.AddMedToolsAuthorizationServices();
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