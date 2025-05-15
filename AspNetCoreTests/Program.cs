using AspNetCoreTests.Features.WeatherForecast;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/weatherforecast", async (IMediator mediator, CancellationToken cancellationToken) =>
    {
        return await mediator.Send(new GetWeatherForecastQuery(), cancellationToken);
    })
    .WithName("GetWeatherForecast");

app.Run();