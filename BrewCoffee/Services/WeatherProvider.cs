using BrewCoffee.Models;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;

namespace BrewCoffee.Services;

public interface IWeatherProvider
{
    Task<Weather> GetWeather();
}

public class WeatherProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "33470c66b015f378c590423a881705c4";

    public WeatherProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Weather> GetWeather()
    {
        var uri = $"{_httpClient.BaseAddress}/weather?units=metric&lat=33.8688&lon=151.2093&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(uri);
        var weather = JsonConvert.DeserializeObject<Weather>(responseString);

        return weather;
    }
}

public static class WeatherServiceExtensions
{
    public static void AddWeatherServices(this IServiceCollection services)
    {
        services.AddHttpClient<IWeatherProvider, WeatherProvider>(client =>
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5");
            })
            .AddPolicyHandler(GetRetryPolicy());
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
