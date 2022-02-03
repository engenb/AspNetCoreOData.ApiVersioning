using System.Reflection;
using AspNetCore.OData.ApiVersioning.ApiExplorer.DependencyInjection;
using AspNetCore.OData.ApiVersioning.DependencyInjection;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using TestHarness.Data;
using TestHarness.Infrastructure.Configuration;
using TestHarness.Infrastructure.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
{
    var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);

    config
        .MinimumLevel.ControlledBy(levelSwitch)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Console()
        .WriteTo.Seq(
            ctx.Configuration.GetSection("Seq")["ServerUrl"],
            apiKey: ctx.Configuration.GetSection("Seq")["ApiKey"],
            controlLevelSwitch: levelSwitch);
});

builder.Host.ConfigureServices((ctx, services) =>
{
    services
        .ConfigureServiceOptions()

        // For in-memory mode...
        //.AddHostedService<DatabaseSeeder>()
        //.AddDbContext<SampleDataDbContext>(opts => opts
        //    .UseInMemoryDatabase("SampleData"));

        // If you don't want to wait for data seed on every startup...
        .AddDbContext<SampleDataDbContext>(opts => opts
            .UseSqlServer(ctx.Configuration.GetConnectionString("Default")));

    services
        .AddAutoMapper(Assembly.GetExecutingAssembly())
        .AddRouting()
        .AddApiVersioning() // see ApiVersioningConfigurator
        .AddVersionedApiExplorer() // see ApiExplorerConfigurator
        .AddSwaggerGen()
        .AddControllers()
        .AddOData() // see ODataConfigurator
        .AddODataApiVersioning()
        .AddODataApiVersioningApiExplorer()
        ;
});

var app = builder.Build();

app.UseODataRouteDebug();

app
    .UseSerilogRequestLogging()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    })
    .UseSwagger()
    .UseSwaggerUI();

app.Run();
