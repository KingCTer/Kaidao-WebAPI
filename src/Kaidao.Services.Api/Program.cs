using Kaidao.Services.Api.ProgramExtensions;
using Serilog;

// SYSTEM: Create WebApplicationBuilder.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//
// ----- Use Serilog -----
builder.UseSerilog();
Log.Information("Configurating WebApplicationBuilder...");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();