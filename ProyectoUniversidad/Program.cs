using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// crear variable para el connectionstring
var connectionString = builder.Configuration.GetConnectionString("ConnectionAWS");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

Log.Information("Servicio iniciado");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


