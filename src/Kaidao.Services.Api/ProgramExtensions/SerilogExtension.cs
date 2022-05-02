using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;

namespace Kaidao.Services.Api.ProgramExtensions;

public static class SerilogExtension
{
    internal static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
            .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration, environment!))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    private static ElasticsearchSinkOptions ConfigureElasticSink(ConfigurationManager configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        };
    }
}