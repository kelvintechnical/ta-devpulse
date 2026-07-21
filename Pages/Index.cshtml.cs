using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DevPulse.Services;
using DevPulse.Models;

namespace DevPulse.Pages;

public class IndexModel : PageModel
{
    private readonly WeatherService _weather;
    private readonly GithubServices _github;
    private readonly CryptoServices _crypto;
    public IndexModel(WeatherService weather, GithubServices github, CryptoServices crypto) 
    {
        _weather = weather;
        _github = github;
        _crypto = crypto;
    }
    public CardResult<WeatherInfo>? Weather {get; private set;}
    public CardResult<Github>? Github {get; private set;}
    public CardResult<Crypto>? Crypto {get; private set;}

    public async Task OnGetAsync() // this is a method that is called when the page is loaded
    {
        Weather = await _weather.GetAsync(); // this is a method that is called to get the weather data
        Github = await _github.GetAsync(); // this is a method that is called to get the Github data
        Crypto = await _crypto.GetAsync(); // this is a method that is called to get the Crypto data
    }
    

}
