using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Services.Api.ProgramExtensions;

public static class DatabaseExtension
{
    internal static WebApplicationBuilder AddDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        // Add AuthDbContext
        builder.Services.AddDbContext<AuthDbContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName));
            if (!builder.Environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        return builder;
    }
}
