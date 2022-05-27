using System.IdentityModel.Tokens.Jwt;
using Kaidao.Web.Admin.ProgramExtensions;
using Kaidao.Web.Share.ApiClient;
using Kaidao.Web.Share.ApiClient.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;


// ----- Database -----
builder.AddDatabaseConfiguration();
// ----- AutoMapper -----
builder.AddAutoMapperConfiguration();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", options =>
    {
        options.AccessDeniedPath = "/AccessDenied";
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5000";

        options.ClientId = "admin";
        options.ClientSecret = "AdminSecret";

        // code flow + PKCE (PKCE is turned on by default)
        options.ResponseType = "code";
        options.UsePkce = true;

        // keeps id_token smaller
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Scope.Add("IdentityServerApi");

        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("permissions", "permissions");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "permissions",

        };

    });


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
});

builder.Services.AddHttpClient(builder.Configuration["KaidaoServicesApi:ClientName"]).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    //if (environment == Environments.Development)
    //{
    //    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    //}
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    return handler;
});

// ----- MediatR -----
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
// ----- DI -----
builder.RegisterServices();

builder.Services.AddTransient<IBaseApiClient, BaseApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute()
        .RequireAuthorization();

    endpoints.MapControllerRoute(
        name: "AccessDenied",
        pattern: "/AccessDenied",
        new { controller = "Access", action = "AccessDenied" });

    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
