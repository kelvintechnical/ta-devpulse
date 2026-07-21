using System.Text.Json;
using DevPulse.Models;

namespace DevPulse.Services;

public class GithubServices //the class which will hold the constructor and methods to get the Github data
{
    private readonly HttpClient _httpClient;

    public GithubServices(HttpClient httpclient)
    {
      _httpClient = httpclient;
    }

    public async Task<CardResult<Github>> GetAsync()  //the method which will get the Github data

{
    try {
        var url = "https://api.github.com/repos/kelvintechnical/ta-devpulse"; 

        using var response = await _httpClient.GetAsync(url); 
        response.EnsureSuccessStatusCode();
        await using var stream = await response.Content.ReadAsStreamAsync();  
        using var doc = await JsonDocument.ParseAsync(stream);  

        var repos = doc.RootElement;
        var name = repos.GetProperty("name").GetString();
        var description = repos.GetProperty("description").GetString();
        var htmlUrl = repos.GetProperty("html_url").GetString();
        var currentDate = repos.GetProperty("created_at").GetString();
        var summary = repos.GetProperty("description").GetString();

        return new CardResult<Github>
        {
            Data = new Github
            {
                Name = name,
                Description = description,
                HtmlUrl = htmlUrl,
                CurrentDate = currentDate,
                Summary = $"{name} — {description ?? "No description"}"
            }
        };
        }
        catch (Exception ex)
        {
            return new CardResult<Github>
            {
                Error = $"Couldn't load Github: {ex.Message}"
            };
        }
        }


}


