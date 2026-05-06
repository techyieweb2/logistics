using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Api.Common;
using TripService.Api.Features.Trips.Create;
using TripService.Infrastructure.Kafka;
using TripService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add this to suppress MediatR license warning
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Database
builder.Services.AddDbContext<TripDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TripDbConn")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateTripHandler).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTripValidator>();

// Pipeline Behavior - runs validation automatically before every handler
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

// Kafka
builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

// Controllers
builder.Services.AddControllers();
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