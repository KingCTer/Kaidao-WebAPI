using Kaidao.Services.Api.ProgramExtensions;
using Serilog;

// SYSTEM: Create WebApplicationBuilder.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//
// ----- Use Serilog -----
builder.UseSerilog();
Log.Information("Configurating WebApplicationBuilder...");
//
// ----- Controllers -----
builder.Services.AddControllersWithViews();
//
// ----- Database -----
builder.AddDatabaseConfiguration();
//
// ----- Swagger UI -----
builder.AddSwaggerConfiguration();


builder.Services.AddEndpointsApiExplorer();


// SYSTEM: Build WebApplication.
Log.Information("Building WebApplication...");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ----- Use Swagger -----
    app.UseSwaggerConfiguration();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("Starting host...");
app.Run();