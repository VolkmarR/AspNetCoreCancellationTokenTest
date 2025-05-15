using MediatR;

namespace AspNetCoreTests.Features.WeatherForecast;

public record GetWeatherForecastQuery() : IRequest<GetWeatherForecastQueryResponse[]>;

public record GetWeatherForecastQueryResponse(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryResponse[]>
{
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly ILogger<GetWeatherForecastHandler> _logger;

    public GetWeatherForecastHandler(ILogger<GetWeatherForecastHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task<GetWeatherForecastQueryResponse[]> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetWeatherForecast before delay");
        
        await Task.Delay(5000, cancellationToken);

        _logger.LogInformation("GetWeatherForecast after delay");
        
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new GetWeatherForecastQueryResponse
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
            
        return forecast;
    }
}
