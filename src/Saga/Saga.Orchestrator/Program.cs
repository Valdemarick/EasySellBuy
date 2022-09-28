using Saga.Orchestrator.IoC;
using Shared.Logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
    .Build();

CustomLoggerConfiguration.ConfigureLogs(configuration);
builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Configuration.AddJsonFile(
    $"clients.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
    optional: false,
    reloadOnChange: true);

builder.Services
    .AddClientOptions(builder.Configuration)
    .AddHttpClients(builder.Configuration)
    .AddDependencies();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
