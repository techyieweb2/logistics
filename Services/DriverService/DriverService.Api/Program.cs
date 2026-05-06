using DriverService.Api.Common;
using DriverService.Api.Features.Drivers.Create;
using DriverService.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add this to suppress MediatR license warning
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Database
builder.Services.AddDbContext<DriverDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DriverDbConn")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateDriverHandler).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateDriverValidator>();

// Pipeline Behavior - runs validation automatically before every handler
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DriverDbContext>();
    db.Database.Migrate();
}

app.Run();