# Slide 2 — Live API Cards

**Status:** Next  
**Slides:** _Google Slides link coming soon_  
**Repo:** [github.com/kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse) · **Site:** [kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)

> **Goal:** Wire real data into each card — typed `HttpClient` services, models, DI, and per-card errors. Layout from Slide 1 stays; data fills the shells.

**Prev:** [Slide 1 — Dashboard Skeleton](../slide-01-dashboard-skeleton/README.md) · **Next:** [Slide 3 — Ship & Close](../slide-03-ship-and-close/README.md) · [← README](../../README.md)

---

## Weather (first HttpClient)

### Step 2 — Weather (first HttpClient call)

---

> **Visual check:** After the checkpoint, compare your browser to the Weather card on your running app. A committed screenshot will be added here after the live demo.

### Goal
Show real weather data on the Weather card.

### Why
This is the first full path: **HTTP request → JSON → C# model → Razor**.

### Mental model
> API = waiter · `HttpClient` = your order · JSON = the plate · model = labels on the plate · Razor = food on your table

### 2A — Create a shared result wrapper
Create folder `DevPulse/Models/` if it does not exist.  
Create file `DevPulse/Models/CardResult.cs` and type:

```csharp
namespace DevPulse.Models;

public class CardResult<T>
{
    public T? Data { get; set; }
    public string? Error { get; set; }
    public bool Ok => Error is null && Data is not null;
}
```

| Syntax | Meaning |
|---|---|
| `namespace DevPulse.Models` | Groups related types under a name |
| `CardResult<T>` | Generic type — `T` is “whatever data this card holds” |
| `T?` / `string?` | Can be null |
| `Ok => ...` | Computed property (no separate field) |

### 2B — Create the weather model
Create `DevPulse/Models/WeatherInfo.cs`:

```csharp
namespace DevPulse.Models;

public class WeatherInfo
{
    public double TemperatureC { get; set; }
    public double WindKph { get; set; }
    public string Summary { get; set; } = "";
}
```

`{ get; set; }` means: this property can be read and written (standard for data models).

### 2C — Create the weather service
Create folder `DevPulse/Services/`.  
Create `DevPulse/Services/WeatherService.cs`:

```csharp
using System.Text.Json;
using DevPulse.Models;

namespace DevPulse.Services;

public class WeatherService
{
    private readonly HttpClient _http;

    public WeatherService(HttpClient http)
    {
        _http = http;
    }

    public async Task<CardResult<WeatherInfo>> GetAsync()
    {
        try
        {
            // Greenville, NC-ish coordinates — change if you like
            var url =
                "https://api.open-meteo.com/v1/forecast?latitude=35.61&longitude=-77.37&current=temperature_2m,wind_speed_10m";

            using var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var current = doc.RootElement.GetProperty("current");
            var temp = current.GetProperty("temperature_2m").GetDouble();
            var wind = current.GetProperty("wind_speed_10m").GetDouble();

            return new CardResult<WeatherInfo>
            {
                Data = new WeatherInfo
                {
                    TemperatureC = temp,
                    WindKph = wind,
                    Summary = $"{temp:0.#}°C · wind {wind:0.#} km/h"
                }
            };
        }
        catch (Exception ex)
        {
            return new CardResult<WeatherInfo> { Error = $"Couldn't load weather: {ex.Message}" };
        }
    }
}
```

| Syntax | Meaning |
|---|---|
| `HttpClient` | Sends HTTP requests |
| `async Task<...>` | Method that can `await` network work |
| `await` | Wait for the network without freezing the thread the wrong way |
| `try` / `catch` | If the API fails, return an error string instead of crashing |
| `JsonDocument` | Read JSON without writing a full matching class for every field |

### 2D — Register the service (dependency injection)
Open `DevPulse/Program.cs`.  
**Right after** `builder.Services.AddRazorPages();` type:

```csharp
builder.Services.AddHttpClient<DevPulse.Services.WeatherService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
```

**Why:** the app creates `WeatherService` for you and gives it a ready `HttpClient`. That’s dependency injection (DI).

### 2E — Call the service from the page model
Open `DevPulse/Pages/Index.cshtml.cs`. **Replace** the whole file with:

```csharp
using DevPulse.Models;
using DevPulse.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevPulse.Pages;

public class IndexModel : PageModel
{
    private readonly WeatherService _weather;

    public IndexModel(WeatherService weather)
    {
        _weather = weather;
    }

    public CardResult<WeatherInfo>? Weather { get; private set; }

    public async Task OnGetAsync()
    {
        Weather = await _weather.GetAsync();
    }
}
```

| Syntax | Meaning |
|---|---|
| Constructor `IndexModel(WeatherService weather)` | Ask DI for the service |
| `OnGetAsync` | Runs when the browser GETs this page |
| `async Task` | Page load can await API calls |

### 2F — Show weather in the card
In `Index.cshtml`, replace the Weather card body (`<p class="muted">Coming next…</p>`) with:

```cshtml
@if (Model.Weather?.Ok == true)
{
    <p>@Model.Weather.Data!.Summary</p>
}
else
{
    <p class="error">@(Model.Weather?.Error ?? "Couldn't load weather.")</p>
}
```

| Syntax | Meaning |
|---|---|
| `@if` | Razor if-statement |
| `Model.Weather` | Property from `Index.cshtml.cs` |
| `?.` | Null-conditional (safe if null) |
| `@(...)` | Print a C# expression into HTML |

### Checkpoint
`dotnet run` → Weather card shows a temperature line (or a clear error).

---
## Hacker News (Task.WhenAll)

### Step 3 — Hacker News (Task.WhenAll)

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

### Goal
Show the top 5 HN story titles (with links).

### Why
One request gets IDs; five more fetch stories. `Task.WhenAll` runs those five together.

### 3A — Model
Create `DevPulse/Models/NewsStory.cs`:

```csharp
namespace DevPulse.Models;

public class NewsStory
{
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
}
```

### 3B — Service
Create `DevPulse/Services/HackerNewsService.cs`:

```csharp
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DevPulse.Models;

namespace DevPulse.Services;

public class HackerNewsService
{
    private readonly HttpClient _http;

    public HackerNewsService(HttpClient http) => _http = http;

    public async Task<CardResult<List<NewsStory>>> GetTopAsync(int count = 5)
    {
        try
        {
            var ids = await _http.GetFromJsonAsync<int[]>(
                "https://hacker-news.firebaseio.com/v0/topstories.json");

            if (ids is null || ids.Length == 0)
                return new CardResult<List<NewsStory>> { Error = "No stories returned." };

            var tasks = ids.Take(count).Select(async id =>
            {
                var item = await _http.GetFromJsonAsync<HnItem>(
                    $"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                return item;
            });

            var items = await Task.WhenAll(tasks);

            var stories = items
                .Where(i => i is not null && !string.IsNullOrWhiteSpace(i.Title))
                .Select(i => new NewsStory
                {
                    Title = i!.Title!,
                    Url = string.IsNullOrWhiteSpace(i.Url)
                        ? $"https://news.ycombinator.com/item?id={i.Id}"
                        : i.Url!
                })
                .ToList();

            return new CardResult<List<NewsStory>> { Data = stories };
        }
        catch (Exception ex)
        {
            return new CardResult<List<NewsStory>> { Error = $"Couldn't load HN: {ex.Message}" };
        }
    }

    private sealed class HnItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
```

| Syntax | Meaning |
|---|---|
| `GetFromJsonAsync<T>` | Download JSON and turn it into type `T` |
| `Take(count)` | Only the first N IDs |
| `Task.WhenAll` | Wait until all story downloads finish |
| `[JsonPropertyName("title")]` | Map JSON field `"title"` to the C# property |

### 3C — Register in `Program.cs`
Under the Weather registration, add:

```csharp
builder.Services.AddHttpClient<DevPulse.Services.HackerNewsService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
```

### 3D — Wire into `Index.cshtml.cs`
- Add field + constructor parameter for `HackerNewsService`  
- Add property: `public CardResult<List<NewsStory>>? HackerNews { get; private set; }`  
- In `OnGetAsync`, add: `HackerNews = await _hn.GetTopAsync();`  

(Inject both services in the constructor.)

### 3E — Razor card
Replace the HN card body with a list of links when `Model.HackerNews?.Ok == true`, else show `Model.HackerNews?.Error`.

Example list:

```cshtml
<ul>
@foreach (var story in Model.HackerNews.Data!)
{
    <li><a href="@story.Url" target="_blank" rel="noopener">@story.Title</a></li>
}
</ul>
```

### Checkpoint
Five HN headlines appear.

---
## GitHub Trending

### Step 4 — GitHub Trending

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

### Goal
Repos created in the last week, sorted by stars.

### Why
Practice date math + query strings + sorting. GitHub requires a `User-Agent` header.

### 4A — Model
`DevPulse/Models/GitHubRepo.cs`:

```csharp
namespace DevPulse.Models;

public class GitHubRepo
{
    public string Name { get; set; } = "";
    public string Url { get; set; } = "";
    public int Stars { get; set; }
}
```

### 4B — Service
`DevPulse/Services/GitHubService.cs`:

```csharp
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DevPulse.Models;

namespace DevPulse.Services;

public class GitHubService
{
    private readonly HttpClient _http;

    public GitHubService(HttpClient http)
    {
        _http = http;
        if (!_http.DefaultRequestHeaders.UserAgent.Any())
        {
            _http.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("DevPulse", "1.0"));
        }
    }

    public async Task<CardResult<List<GitHubRepo>>> GetTrendingAsync()
    {
        try
        {
            var since = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd");
            var url =
                $"https://api.github.com/search/repositories?q=created:>{since}&sort=stars&order=desc&per_page=5";

            var payload = await _http.GetFromJsonAsync<GitHubSearchResponse>(url);
            var repos = payload?.Items?
                .Select(i => new GitHubRepo
                {
                    Name = i.FullName ?? i.Name ?? "unknown",
                    Url = i.HtmlUrl ?? "#",
                    Stars = i.Stars
                })
                .ToList() ?? [];

            return new CardResult<List<GitHubRepo>> { Data = repos };
        }
        catch (Exception ex)
        {
            return new CardResult<List<GitHubRepo>> { Error = $"Couldn't load GitHub: {ex.Message}" };
        }
    }

    private sealed class GitHubSearchResponse
    {
        [JsonPropertyName("items")]
        public List<GitHubItem>? Items { get; set; }
    }

    private sealed class GitHubItem
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }
    }
}
```

### 4C — Register + page model + Razor
Same pattern as Weather/HN:

1. `AddHttpClient<GitHubService>(...)` in `Program.cs`  
2. Inject + call in `OnGetAsync`  
3. Render name, stars, and link in the GitHub card  

### Checkpoint
Repos + star counts show (or a clear rate-limit error).

---
## Crypto (auto-refresh)

### Step 5 — Crypto (auto-refresh)

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

### Goal
Show BTC and ETH prices; page refreshes about every 60 seconds.

### Why
Teach “keep data fresh” without a complex SPA. Meta-refresh is simple for a lab.

### 5A — Model
`DevPulse/Models/CryptoPrices.cs`:

```csharp
namespace DevPulse.Models;

public class CryptoPrices
{
    public decimal BtcUsd { get; set; }
    public decimal EthUsd { get; set; }
}
```

### 5B — Service
`DevPulse/Services/CryptoService.cs`:

```csharp
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DevPulse.Models;

namespace DevPulse.Services;

public class CryptoService
{
    private readonly HttpClient _http;
    public CryptoService(HttpClient http) => _http = http;

    public async Task<CardResult<CryptoPrices>> GetAsync()
    {
        try
        {
            var url =
                "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";
            var data = await _http.GetFromJsonAsync<CoinGeckoResponse>(url);

            if (data?.Bitcoin is null || data.Ethereum is null)
                return new CardResult<CryptoPrices> { Error = "Unexpected crypto payload." };

            return new CardResult<CryptoPrices>
            {
                Data = new CryptoPrices
                {
                    BtcUsd = data.Bitcoin.Usd,
                    EthUsd = data.Ethereum.Usd
                }
            };
        }
        catch (Exception ex)
        {
            return new CardResult<CryptoPrices> { Error = $"Couldn't load crypto: {ex.Message}" };
        }
    }

    private sealed class CoinGeckoResponse
    {
        [JsonPropertyName("bitcoin")]
        public CoinPrice? Bitcoin { get; set; }

        [JsonPropertyName("ethereum")]
        public CoinPrice? Ethereum { get; set; }
    }

    private sealed class CoinPrice
    {
        [JsonPropertyName("usd")]
        public decimal Usd { get; set; }
    }
}
```

### 5C — Register + page model + Razor
Same DI pattern. Show something like:

```text
BTC: $xx,xxx
ETH: $x,xxx
```

### 5D — Meta refresh
In `Index.cshtml`, **inside** the `@{ ViewData["Title"] = "DevPulse"; }` block is fine for title only.  
For refresh, add this in the page (top of body content is OK), or in the layout `<head>` via a section.

Simplest for the lab — add under `@page` section in `Index.cshtml`:

```cshtml
@section Head {
    <meta http-equiv="refresh" content="60" />
}
```

Then in `_Layout.cshtml` `<head>`, after other CSS links, add:

```cshtml
@await RenderSectionAsync("Head", required: false)
```

**Why 60s:** be kind to free APIs.

### Checkpoint
Prices show; after ~60s the page reloads.

---
## Daily Challenge (no API)

### Step 6 — Daily Challenge (no API)

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

### Goal
Button picks a random challenge from a local list.

### Why
Not everything needs the network. Practice a Razor Pages **handler** (form post).

### 6A — Service
`DevPulse/Services/ChallengeService.cs`:

```csharp
namespace DevPulse.Services;

public class ChallengeService
{
    private static readonly string[] Challenges =
    [
        "Explain an API like you're teaching a friend.",
        "Rename one unclear variable in your code.",
        "Add a better error message to one card.",
        "Sketch the request/response flow on paper.",
        "Commit your work with a clear message.",
        "Change the Weather coordinates to your city.",
        "Add one more coin to the Crypto card.",
        "Help a neighbor debug a red error."
    ];

    public string PickRandom()
    {
        var index = Random.Shared.Next(Challenges.Length);
        return Challenges[index];
    }
}
```

### 6B — Register (no HttpClient)
In `Program.cs`:

```csharp
builder.Services.AddSingleton<DevPulse.Services.ChallengeService>();
```

`AddSingleton` = one shared instance for the app (fine for a static list).

### 6C — Page model handler
In `Index.cshtml.cs`:

- Inject `ChallengeService`  
- Property: `public string? Challenge { get; private set; }`  
- In `OnGetAsync`, you may set an initial challenge: `Challenge = _challenges.PickRandom();`  
- Add handler:

```csharp
public void OnPostNewChallenge()
{
    Challenge = _challenges.PickRandom();
    // Re-load other cards too if you want a full refresh after post —
    // simplest lab approach: redirect back to GET
}
```

**Easier lab pattern** — use redirect:

```csharp
public IActionResult OnPostNewChallenge()
{
    TempData["Challenge"] = _challenges.PickRandom();
    return RedirectToPage();
}
```

And in `OnGetAsync`:

```csharp
Challenge = TempData["Challenge"] as string ?? _challenges.PickRandom();
```

Add `using Microsoft.AspNetCore.Mvc;` if needed for `IActionResult` / `RedirectToPage`.

**Note:** After a POST redirect, also re-fetch Weather/HN/etc. in `OnGetAsync` as you already do.

### 6D — Razor form
Challenge card:

```cshtml
<p>@(Model.Challenge ?? "Click for a challenge.")</p>
<form method="post" asp-page-handler="NewChallenge">
    <button type="submit">New challenge</button>
</form>
```

| Syntax | Meaning |
|---|---|
| `method="post"` | Send a POST (not a GET) |
| `asp-page-handler="NewChallenge"` | Calls `OnPostNewChallenge` |

### Checkpoint
Clicking the button shows a new challenge string.

---

## Slide 2 checkpoint

- [ ] Weather shows temperature (or a clear error)
- [ ] Hacker News shows top 5 titles
- [ ] GitHub Trending shows repos + stars
- [ ] Crypto shows BTC/ETH
- [ ] Daily Challenge form works locally

**When finished →** [Slide 3 — Ship & Close](../slide-03-ship-and-close/README.md)
