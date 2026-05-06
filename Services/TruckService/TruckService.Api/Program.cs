using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Api.Common;
using TruckService.Api.Features.Trucks.Create;
using TruckService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add this to suppress MediatR license warning
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Database
builder.Services.AddDbContext<TruckDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TruckDbConn")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateTruckHandler).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTruckValidator>();

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
    var db = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
    db.Database.Migrate();
}

app.Run();