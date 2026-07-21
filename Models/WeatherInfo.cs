namespace DevPulse.Models;

public class WeatherInfo 
{
 public double TemperatureC {get;set;}
//label slot on the report card
// holds the temperature in Celsius
// Gotcha: double is not nullable, so if we don't set a default value, it will default to 0.0 
//which looks like zero degress, not 'no data was ever set

 public double WindKph { get; set; }

 //label slot on the report card
 // holds the wind speed in kilometers per hour
 // Gotcha: double is not nullable, so if we don't set a default value, it will default to 0.0 
 //which looks like zero kilometers per hour, not 'no data was ever set

 
 public string? Summary {get;set;} = "";
 // default value of "" is a string that is empty
 //without the = "", the  weather card would first show Partly Cloudy. Defaulting
 // to "" guarantees its never null, so calling .ToUpper() or displaying it in Razor never throwsa NullReferenceException. 
}