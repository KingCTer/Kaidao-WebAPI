using Kaidao.Services.Api.ProgramExtensions;
using Serilog;

SerilogExtension.BootstrapLogger("Starting up");

try
{
    // SYSTEM: Create WebApplicationBuilder.
    var builder = WebApplication.CreateBuilder(args);

    // ----- Use Serilog -----
    builder.UseSerilog();

    // Configure Services.
    //
    // ----- Controllers And Views -----
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    //
    // ----- Database -----
    builder.AddDatabaseConfiguration();
    //
    // ----- Identity -----
    builder.AddIdentityConfiguration();
    //
    // ----- Auth -----
    builder.AddAuthConfiguration();
    //
    // ----- Cors -----
    builder.Services.AddCors();
    //
    // ----- Swagger UI -----
    builder.AddSwaggerConfiguration();
    //
    // ----- DI -----
    builder.RegisterServices();


    builder.Services.AddEndpointsApiExplorer();


    // SYSTEM: Build WebApplication.
    Log.Information("Building WebApplication...");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        // ----- Use Swagger -----
        app.UseSwaggerConfiguration();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();

    app.UseAuthorization();

    app.UseCors(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.MapRazorPages()
        .RequireAuthorization();

    app.MapControllers();


    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        app.SeedDatabase();
        Log.Information("Done seeding database. Exiting.");
        return;
    }


    Log.Information("Starting host...");
    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

