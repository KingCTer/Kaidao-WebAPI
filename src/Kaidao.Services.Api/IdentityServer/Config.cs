using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Kaidao.Domain.Constants;

namespace Kaidao.Services.Api.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            //new MyIdentityResources.Permissions(),
            //new MyIdentityResources.Roles(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName, "Public Api Scope"),
            new ApiScope(MyIdentityServerConstants.PrivateApi.ScopeName, "Private Api Resource"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "swagger",
                ClientSecrets = { new Secret("SwaggerSecret".Sha256()) },
                ClientName = "Swagger Client",

                AllowedGrantTypes = GrantTypes.Implicit,

                AllowAccessTokensViaBrowser = true,

                RequireConsent = false,

                RedirectUris = { "https://localhost:5000/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { "https://localhost:5000/swagger/oauth2-redirect.html" },

                AllowedCorsOrigins = { "https://localhost:5000" },

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.LocalApi.ScopeName,
                    MyIdentityServerConstants.PrivateApi.ScopeName,
                }
            },
        };
}