using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;


public class YandexWeather
{
    private const string API_KEY = "e91dc37d-e5cb-4de8-b69b-d8951d5c3a1c";
    private const string API_URL = "https://api.weather.yandex.ru/v2/forecast?lat={0}&lon={1}";

    public static async Task Main()
    {
       
        double lat = 55.755864;
        double lon = 37.617698;
        WeatherInfo weather= new WeatherInfo();

        weather = await YandexWeather.GetWeatherAsync(lat, lon);
        Console.WriteLine("Температура: {0}°C", weather.Temp);
        Console.WriteLine("Состояние: {0}", weather.Condition);
        Console.WriteLine("Скорость ветра: {0} м/с", weather.WindSpeed);
        Console.WriteLine("Влажность: {0}%", weather.Humidity);
        
 
        
    }
    public static async Task<WeatherInfo> GetWeatherAsync(double lat, double lon)
    {
        string url = string.Format(CultureInfo.InvariantCulture ,API_URL, lat, lon);
        

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-Yandex-API-Key", API_KEY);
            
            

            HttpResponseMessage response = await client.GetAsync(url);

            

            string responseBody = await response.Content.ReadAsStringAsync();

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);

            return weatherResponse.Fact;
        }
    }
}

public class WeatherResponse
{
    public WeatherInfo Fact { get; set; }
}

public class WeatherInfo
{
    public int Temp { get; set; }
    public string Condition { get; set; }
    public double WindSpeed { get; set; }
    public int Humidity { get; set; }
}