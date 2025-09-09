using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Properties;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public IActionResult GetCurrentWeather()
    {
        var result = _weatherService.GetWeatherSync(); // ⚠️ .Result внутри
        return Ok(result);
    }
}