using Duende.IdentityServer;

namespace Kaidao.Services.Api.ProgramExtensions;

public static class AuthExtension
{
    public static WebApplicationBuilder AddAuthConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddLocalApiAuthentication();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5000/signin-google
                options.ClientId = "1014368622629-8jo2dqjroonrj7p44sr37v092dvbr3am.apps.googleusercontent.com";
                options.ClientSecret = "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";
            })
            .AddLocalApi("PrivateAccessToken", option =>
            {
                option.ExpectedScope = "PrivateApi";
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("PrivateAccessToken", policy =>
            {
                policy.AddAuthenticationSchemes("PrivateAccessToken");
                policy.RequireAuthenticatedUser();
                //custom requirements
            });
        });

        return builder;
    }
}