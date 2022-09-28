using Ad.Application.IoC;
using Ad.Infrastructure.Persistence.IoC;
using Ad.WebApi.IoC;
using Exceptions.IoC;
using Serilog;
using Shared.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
    .Build();

CustomLoggerConfiguration.ConfigureLogs(configuration);
builder.Host.UseSerilog();

builder.Services.AddApplication()
                .AddData(builder.Configuration)
                .AddKafka(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();