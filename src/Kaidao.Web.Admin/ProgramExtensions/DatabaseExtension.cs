using Kaidao.Infra.CrossCutting.Identity.Context;
using Kaidao.Infra.CrossCutting.Identity.Seeds;
using Kaidao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Web.Admin.ProgramExtensions;

public static class DatabaseExtension
{
    internal static WebApplicationBuilder AddDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        // Add AuthDbContext
        builder.Services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName));
            if (!builder.Environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        // Add ApplicationDbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            if (!builder.Environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        // Add EventStoreSqlContext
        builder.Services.AddDbContext<EventStoreSqlContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(EventStoreSqlContext).Assembly.FullName));
            if (!builder.Environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        return builder;
    }

    internal static WebApplication SeedDatabase(this WebApplication app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        SeedUsers.EnsureSeedUsers(app);

        return app;
    }
}
