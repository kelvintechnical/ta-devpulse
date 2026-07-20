# DevPulse — Build Guide (you type this)

**Tech Affiliates lab · ASP.NET Core Razor Pages · .NET 8 · C#**

This README is the lab. **You** create the files and type the code.  
Do **not** ask an AI to “just finish the app” — that skips the learning.

Teaching notes for slides live in [`decks/`](decks/). Convert those to PowerPoint **after** you finish a full local run.

---

## What you will build

A dashboard with five cards:

1. Weather (Open-Meteo)  
2. Hacker News (top 5)  
3. GitHub Trending (last week, by stars)  
4. Crypto (CoinGecko, auto-refresh)  
5. Daily Challenge (local list + button)

**Rules:** no database, no API keys, beginner-readable C#, services separate from the page UI.  
**Deploy target:** Azure App Service (Step 7).

**Live demo URL (add yours after deploy):** `_your-azure-url-here_`

---

## How to use this guide

For every step:

1. Read **Goal** and **Why**.  
2. Type what the step says (in Cursor / terminal).  
3. Run and hit the **Checkpoint**.  
4. Optional: save a screenshot in `screenshots/` (e.g. `01-skeleton.png`).  
5. Only then go to the next step.

---

## Before Step 0 (pre-lab)

Install these **before** the session:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
  Check: open a terminal and run:

```bash
dotnet --version
```

You should see something like `8.0.x`.

- Cursor or VS Code with C# support  
- A free [Azure](https://azure.microsoft.com/free/) account (for Step 7; can finish later if signup is slow)  
- This repo cloned or opened on your machine  

---

## Step 0 — Setup

### Goal
Run a Razor Pages app in the browser.

### Why
You need a working project before any APIs. `dotnet` is the tool that creates, builds, and runs C# web apps.

### If the `DevPulse/` folder is already in this repo
Someone may have run the template once for you. That’s only scaffolding (default Welcome page). Open it and run:

```bash
cd DevPulse
dotnet run
```

### If you are creating the project yourself
From the repo root (`ta-devpulse` / `DevPulse` parent folder):

```bash
dotnet new webapp -n DevPulse -o DevPulse
cd DevPulse
dotnet run
```

### What those commands mean

| Command | Meaning |
|---|---|
| `dotnet new webapp` | Create a new Razor Pages web app from a template |
| `-n DevPulse` | Project name |
| `-o DevPulse` | Output folder name |
| `dotnet run` | Build and start a local web server |

### Syntax peek — `Program.cs`
Open `DevPulse/Program.cs`. You should see something like:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app = builder.Build();
// ...
app.MapRazorPages();
app.Run();
```

| Piece | Meaning |
|---|---|
| `var builder = ...` | Starts configuring the web app |
| `AddRazorPages()` | Turns on Razor Pages |
| `app.Run()` | Starts listening for browser requests |

### Checkpoint
- Terminal prints a URL like `http://localhost:5xxx` or `https://localhost:7xxx`  
- Opening it shows the default Welcome page  

**Screenshot:** `screenshots/00-setup.png`

Stop the server with `Ctrl+C` when you move to the next edit (or leave it running and restart after big changes).

---

## Step 1 — The skeleton (five empty cards)

### Goal
Show five labeled empty cards in a grid. No API calls yet.

### Why
Layout first. If the page structure is wrong, data will not save you.

### 1A — Replace the home page HTML
Open `DevPulse/Pages/Index.cshtml`.

**Delete** the Welcome content. **Type** this instead:

```cshtml
@page
@model IndexModel
@{
    ViewData["Title"] = "DevPulse";
}

<section class="hero">
    <h1>DevPulse</h1>
    <p class="tagline">Live developer pulse — APIs in C#</p>
</section>

<section class="card-grid">
    <article class="card" id="weather">
        <h2>Weather</h2>
        <p class="muted">Coming next…</p>
    </article>

    <article class="card" id="hn">
        <h2>Hacker News</h2>
        <p class="muted">Coming next…</p>
    </article>

    <article class="card" id="github">
        <h2>GitHub Trending</h2>
        <p class="muted">Coming next…</p>
    </article>

    <article class="card" id="crypto">
        <h2>Crypto</h2>
        <p class="muted">Coming next…</p>
    </article>

    <article class="card" id="challenge">
        <h2>Daily Challenge</h2>
        <p class="muted">Coming next…</p>
    </article>
</section>
```

**Syntax notes**

| Syntax | Meaning |
|---|---|
| `@page` | This file is a Razor Page (routable) |
| `@model IndexModel` | Links this view to `Index.cshtml.cs` |
| `@{ ... }` | A C# code block inside the page |
| `<section>`, `<article>` | HTML landmarks for layout |

### 1B — Add CSS for the grid
Open `DevPulse/wwwroot/css/site.css`.

**Add** these rules at the **bottom** (keep existing rules if you want):

```css
body {
  font-family: "Segoe UI", system-ui, sans-serif;
  background: #0f1419;
  color: #e7ecf1;
}

.hero {
  text-align: center;
  padding: 2rem 1rem 1rem;
}

.hero h1 {
  margin: 0;
  font-size: 2.5rem;
  letter-spacing: 0.04em;
}

.tagline {
  color: #9aa7b5;
  margin-top: 0.5rem;
}

.card-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(260px, 1fr));
  gap: 1rem;
  padding: 1rem;
  max-width: 1100px;
  margin: 0 auto 3rem;
}

.card {
  background: #1a222c;
  border: 1px solid #2c3845;
  border-radius: 12px;
  padding: 1rem 1.25rem;
  min-height: 160px;
}

.card h2 {
  margin: 0 0 0.75rem;
  font-size: 1.1rem;
  color: #7dd3fc;
}

.muted { color: #9aa7b5; }
.error { color: #fca5a5; }
.card ul { padding-left: 1.1rem; margin: 0; }
.card li { margin-bottom: 0.35rem; }
.card a { color: #93c5fd; }
```

### 1C — Optional: simplify the nav
Open `DevPulse/Pages/Shared/_Layout.cshtml`. You can leave Bootstrap nav as-is for the lab, or remove the Privacy link later. Not required for the checkpoint.

### Checkpoint
Run:

```bash
cd DevPulse
dotnet run
```

You should see **five empty cards**.

**Screenshot:** `screenshots/01-skeleton.png`

---

## Step 2 — Weather (first `HttpClient` call)

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

**Screenshot:** `screenshots/02-weather.png`

---

## Step 3 — Hacker News (`Task.WhenAll`)

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

**Screenshot:** `screenshots/03-hackernews.png`

---

## Step 4 — GitHub Trending

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

**Screenshot:** `screenshots/04-github.png`

---

## Step 5 — Crypto (auto-refresh)

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

**Screenshot:** `screenshots/05-crypto.png`

---

## Step 6 — Daily Challenge (no API)

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

**Screenshot:** `screenshots/06-challenge.png`

---

## Step 6.5 — Polish (portfolio bar)

Do these before you call the project “done”:

1. **Every card** has success UI **or** a clear `.error` message (never blank).  
2. **Timeouts** on all `HttpClient` registrations (10s is fine).  
3. **README live URL** placeholder filled after Azure deploy.  
4. Optional: last-updated time: `@DateTime.Now.ToString("t")` in the hero.  
5. Optional: trim Privacy nav link if you do not need it.

---

## Step 7 — Ship to Azure

### Goal
Public HTTPS URL for your portfolio and the lab close.

### Why
“It runs on my laptop” ≠ shipped. Azure App Service hosts the compiled app.

### High-level path (pick one)

**Option A — Azure Portal**
1. Create a free/student Azure account.  
2. Create a Web App (Runtime: .NET 8).  
3. Use Deployment Center → GitHub, or download publish profile.  
4. Publish from Visual Studio / `az webapp up` / GitHub Actions.

**Option B — Azure CLI (sketch)**

```bash
# Login and follow Azure docs for your subscription
az login
# Create resource group + webapp, then deploy
# See: https://learn.microsoft.com/azure/app-service/quickstart-dotnetcore
```

### Checkpoint
Open the Azure URL on your phone → all five cards load.

Put that URL at the top of this README under **Live demo URL**.

**Screenshot:** `screenshots/07-shipped.png`

---

## Architecture (what “good” looks like)

```text
Browser
  → Pages/Index.cshtml (.cshtml.cs)
       → WeatherService / HackerNewsService / GitHubService / CryptoService / ChallengeService
            → Open-Meteo / HN / GitHub / CoinGecko / local list
```

- **Pages** = UI + orchestration  
- **Services** = talking to APIs  
- **Models** = shapes of data  
- **No database** for v1  

---

## Concepts you can put on a resume / LinkedIn

- ASP.NET Core Razor Pages  
- C# `HttpClient` and `async`/`await`  
- JSON deserialization to models  
- Parallel requests with `Task.WhenAll`  
- Dependency injection basics  
- Deploying .NET to Azure  

One-liner: *Built and deployed a multi-API Razor Pages dashboard in C#.*

---

## Stuck?

| Symptom | Try |
|---|---|
| `dotnet` not found | Install .NET 8 SDK; restart terminal |
| Port in use | Stop old `dotnet run` (`Ctrl+C`) |
| Null / empty card | Log or breakpoint; check JSON property names |
| GitHub 403 | Missing `User-Agent`, or rate limit — wait and retry |
| Red build errors | Read the first error only; fix that file |

---

## Facilitator notes

- Night-before email: install .NET 8 SDK.  
- Keep a backup live Azure URL if machines fail.  
- Slide decks: [`decks/01-kickoff`](decks/01-kickoff/slides.md), [`decks/02-build`](decks/02-build/slides.md), [`decks/03-ship-close`](decks/03-ship-close/slides.md) — convert to PPT **after** you personally complete Steps 0–7.  
- **Do not** pre-build Services/Models for attendees; they type from this README.

---

## License / credit

Tech Affiliates · Build in public · Greenville NC
