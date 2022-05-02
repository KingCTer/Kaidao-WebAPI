using Kaidao.Domain.IdentityEntity;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Kaidao.Services.Api.IdentityServer;
using Microsoft.AspNetCore.Identity;

namespace Kaidao.Services.Api.ProgramExtensions;

public static class IdentityExtension
{
    internal static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;
        })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<AppUser>()
            .AddProfileService<IdentityProfileService>()
            .AddDeveloperSigningCredential();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            //options.Password.RequiredLength = 8;
            //options.Password.RequireDigit = true;
            //options.Password.RequireUppercase = true;
            //options.User.RequireUniqueEmail = true;
        });

        return builder;
    }
}
