using DispatcherService.Api.Common;
using DispatcherService.Api.Features.Dispatches.Create;
using DispatcherService.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add this to suppress MediatR license warning
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Database
builder.Services.AddDbContext<DispatcherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DispatcherDbConn")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateDispatchHandler).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateDispatchValidator>();

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
app.Run();