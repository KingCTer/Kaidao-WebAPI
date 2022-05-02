using Duende.IdentityServer;
using Kaidao.Domain.Constants;
using Microsoft.OpenApi.Models;

namespace Kaidao.Services.Api.ProgramExtensions;

public static class SwaggerExtension
{
    internal static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kaidao API",
                Version = "v1",
                Description = "Copyright (c) 2022 ctOS-Network. All rights reserved.",
                TermsOfService = new Uri("https://raw.githubusercontent.com/KingCTer/Kaidao-WebAPI/main/README.md"),
                Contact = new OpenApiContact { Name = "KingCTer", Email = "ctosnetworkvn@gmail.com" },
                License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://raw.githubusercontent.com/KingCTer/Kaidao-WebAPI/main/LICENSE.md") },
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["Authorization:authorization_endpoint"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { IdentityServerConstants.LocalApi.ScopeName, "Public Api Scope" },
                            { MyIdentityServerConstants.PrivateApi.ScopeName, "Private Api Scope" },
                        },
                    },
                }
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new List<string>
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        MyIdentityServerConstants.PrivateApi.ScopeName,
                    }
                }
            });
        });

        return builder;
    }

    internal static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kaidao API v1");
            c.OAuthClientId("swagger");
            c.OAuthClientSecret("SwaggerSecret");
            c.OAuthScopes(
                IdentityServerConstants.LocalApi.ScopeName,
                MyIdentityServerConstants.PrivateApi.ScopeName
            );
        });

        return app;
    }
}