namespace DevPulse.Models;

public class Crypto 
{
    public string? Symbol {get; set;}
    public string? Name {get; set;}
    public double? Price {get; set;}
    public string? Change {get; set;}
    public double? ChangePercent {get; set;}
    public string? MarketCap {get; set;}
    public double? Volume {get; set;}
    public DateTime? LastUpdated {get; set;}
    public string? Summary {get; set;} = "";
}