using Bag.Application.IoC;
using Bag.Persistence.IoC;
using Bag.WebApi.IoC;
using Exceptions.IoC;
using Microsoft.AspNetCore.OData;
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

builder.Services.AddApplication(builder.Configuration)
                .AddPersistence(builder.Configuration)
                .AddKafka(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddControllers()
                .AddOData(opts =>
                                opts
                                .Select()
                                .OrderBy()
                                .Filter()
                                .Expand()); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.SeedDataAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();