using System.Text.Json; // If we intend to use JSON
using DevPulse.Models;   // It unlocks the types in that namespace (WeatherInfo, 
// CardResult<T>) so this file can mention them by name.

namespace DevPulse.Services; // think of namespace like an address for the code we are writing 
// this is the address for the WeatherService class  
// it is a container for the WeatherService class

public class WeatherService
{
    private readonly HttpClient _httpClient; // a type (a class from .NET) that can send HTTP requests

    //its essentially an empty slot for the HttpClient to be stored in 
// _httpClient = a field (storage) of that type


    public WeatherService(HttpClient httpClient) //this is a constructor, which is a special method that is used to initialize objects 
    {
        _httpClient = httpClient; 
    }

    public async Task<CardResult<WeatherInfo>> GetAsync()
    {
        try {

                var url =
                    "https://api.open-meteo.com/v1/forecast?latitude=35.61&longitude=-77.37&current=temperature_2m,wind_speed_10m";

                    using var response = await _httpClient.GetAsync(url);   
                    //use the field to get the HTTP GET; await waits for the network response to complete
                    //holds the response and discards it when done 
                    response.EnsureSuccessStatusCode(); // this is a method that throws an exception if the response is not successful

                    await using var stream = await response.Content.ReadAsStreamAsync();  //reads the response content as a stream of bytes
                    //why a stream? because it is a sequence of bytes that can be read from or written to 
                    //await reads the stream asynchronously  
                    using var doc = await JsonDocument.ParseAsync(stream); //parses the JSON document asynchronously    
                    //why a JsonDocument? because it is a JSON document that can be parsed into a JSON object  

                    var current = doc.RootElement.GetProperty("current");  
                    var temp = current.GetProperty("temperature_2m").GetDouble();
                    var wind = current.GetProperty("wind_speed_10m").GetDouble();

                    return new CardResult<WeatherInfo>
                    {
                        Data = new WeatherInfo 
                        {
                            TemperatureC = temp,
                            WindKph = wind, 
                            Summary = $"{temp:0.#}°C · wind {wind:0.#} km/h",
                        }
                    };
                        }
   
            catch (Exception ex)
            {
                return new CardResult<WeatherInfo>
                {
                    Error = $"Couldn't load weather: {ex.Message}"
                };
            }
    } 

}