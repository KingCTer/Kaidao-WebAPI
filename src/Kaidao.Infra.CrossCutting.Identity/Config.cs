using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Kaidao.Domain.Constants;

namespace Kaidao.Infra.CrossCutting.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            //new MyIdentityResources.Permissions(),
            //new MyIdentityResources.Roles(),
            new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
            new IdentityResource("permissions", "User permissions(s)", new List<string> { "permission" })
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

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "admin",
                ClientSecrets = { new Secret("AdminSecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RequireConsent = false,
                RequirePkce = true,
                AllowOfflineAccess = true,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5001/signin-oidc" },

                FrontChannelLogoutUri = "https://localhost:5001/signout-oidc",

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    MyIdentityServerConstants.PrivateApi.ScopeName,
                    "roles",
                    "permissions"
                }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "portal",
                ClientSecrets = { new Secret("PortalSecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RequireConsent = false,
                RequirePkce = true,
                AllowOfflineAccess = true,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    MyIdentityServerConstants.PrivateApi.ScopeName,
                    "roles",
                    "permissions"
                }
            },
        };
}