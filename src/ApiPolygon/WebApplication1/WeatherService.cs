namespace WebApplication1;

public interface IWeatherService
{
    string GetWeatherSync();
    Task<string> GetWeatherAsync();
}

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string GetWeatherSync()
    {
        var task = GetWeatherAsync();
        var syncContext = SynchronizationContext.Current;

        return syncContext == null ? task.GetAwaiter().GetResult() : Task.Run(() => task).GetAwaiter().GetResult();
    }

    public async Task<string> GetWeatherAsync()
    {
        var response = await _httpClient.GetStringAsync("https://httpbin.org/delay/2").ConfigureAwait(false);
        return $"Weather: {response.Length} chars";
    }
}