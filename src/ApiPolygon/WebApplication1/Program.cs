using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

app.MapControllers();
app.Run();