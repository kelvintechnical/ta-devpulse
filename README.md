# DevPulse

**Live multi-API developer dashboard in ASP.NET Core** · Built in public · Aimed at mid-level .NET

> One-liner for LinkedIn / resume (target when Phase 2 ships):  
> *Built a production-minded ASP.NET Core dashboard that aggregates live developer signals via typed HttpClient services, caching, per-source failure isolation, EF Core prefs, auth, automated tests, and Azure CI/CD.*

**Live demo:** `_your-azure-url-here_`  
**Target roles:** Mid-level .NET / ASP.NET Core · Backend (C#) · Full-stack (.NET + Razor)  
**Stack (target):** ASP.NET Core Razor Pages · .NET 8 · C# · EF Core · Identity/OAuth · Azure App Service · GitHub Actions  
**v1 lab rules:** Beginner-readable · Services separated from UI · Public APIs first (no keys required for the live build)

---

## Current status (mid-tier build)

This repo is a **mid-level portfolio project in progress** — not a toy todo app. The end state is interview-credible ASP.NET Core: typed services, resilience, caching, EF Core, auth, tests, and Azure CI/CD (see Phases 2–3 below).

| Layer | Status | Notes |
|---|---|---|
| Razor Pages shell + dark UI | **Done** | Hero + five card shells; `wwwroot/css/site.css` theme + grid + card look |
| Teaching / slide notes | **Local (facilitator)** | Slide decks & notes are gitignored — not in the student repo |
| Live API cards (Weather → Challenge) | **Next** | Step 2 onward — typed `HttpClient`, DI, per-card errors |
| Mid-level extras (cache, EF, auth, tests, CI) | Planned | Phase 2–3 after v1 ships |

**Overall progress:** ~**35–40%** of the mid-tier product (UI shell complete; zero live APIs yet).  
**Slide One deck (skeleton + CSS):** ~**95%** done.

### Repo layout

```text
DevPulse/                 # git root
  README.md               # this student lab guide
  DevPulse/               # ASP.NET Core Razor Pages app
    Pages/
    wwwroot/
    Program.cs
    screenshots/          # optional student checkpoint shots
  facilitator/            # LOCAL ONLY (gitignored) — slides, notes, marketing
```

### Session — 2026-07-20 (today)

Shipped the **dashboard skeleton** end-to-end:

- Replaced the Welcome page with `Pages/Index.cshtml` — Razor directives (`@page`, `@model`, `@{ }`), hero, and five placeholder cards (`weather`, `hn`, `github`, `crypto`, `challenge`)
- Clarified comment territories: `//` inside C# blocks vs `<!-- -->` in markup
- Rebuilt `wwwroot/css/site.css` from a clean base: dark `body`, `.hero` / `.tagline`, `.card-grid` (`display: grid`, columns, `gap`, `padding`, `max-width`, centered with `margin: 0 auto 3rem`)
- Taught responsive `auto-fit` / `minmax`, then switched to `repeat(5, 1fr)` for the lab projector layout
- Styled `.card` panels (background, olive border, radius, min-height) plus `.card h2`, `.muted`, `.error`, list, and link rules ready for API data
- Facilitator slide notes + screenshots kept local (gitignored) so the student repo stays focused on the app

**Next session:** wire **Weather** first (Step 2 — `HttpClient` + model + service + DI).

---

## Why this project (hiring signal)

| Mid-level signal | How DevPulse shows it |
|---|---|
| Real backend integration | Typed `HttpClient` services → JSON → models → Razor UI across multiple APIs |
| Clean architecture | Services + models + pages; DI in `Program.cs`; page stays thin |
| Async / concurrency | `async`/`await`, `Task.WhenAll` for parallel HN fetches |
| Resilience | Per-card failures, timeouts, retries/backoff — one dead API never kills the page |
| Caching & rate limits | TTL cache so refreshes don’t hammer upstream APIs |
| Persistence | EF Core for prefs / favorites / challenge history (migrations included) |
| Auth | Sign-in (Identity or GitHub OAuth) for a personalized dashboard |
| Quality bar | Unit + integration tests; health checks; structured logging |
| Ship mentality | Azure deploy + GitHub Actions CI (build → test → deploy) |

---

## What it does

Five cards on one dashboard:

1. **Weather** — Open-Meteo (HTTP + JSON parse)
2. **Hacker News** — top 5 via parallel requests (`Task.WhenAll`)
3. **GitHub Trending** — last week, sorted by stars (User-Agent + query design)
4. **Crypto** — CoinGecko BTC/ETH with auto-refresh
5. **Daily Challenge** — local list + Razor Pages form handler (no API)

```text
Browser → Index (Razor Page)
            → Weather / HN / GitHub / Crypto / Challenge services
                 → public APIs or local list
                      → (mid-level) cache · DB · auth · health · CI
```

---

## Screenshots

| Skeleton | Weather | Full board (shipped) |
|---|---|---|
| *(add `screenshots/01-skeleton.png` after your run)* | `screenshots/02-weather.png` | `screenshots/07-shipped.png` |

Optional: save your own checkpoint screenshots under `screenshots/` as you build (do not commit facilitator slide decks).

---

## Run locally

From the **repo root**:

```bash
cd DevPulse
dotnet run
```

Open the localhost URL printed in the terminal (.NET 8 SDK required).

---

## Roadmap — junior v1 → mid-level

Build in this order. **v1** is the live lab (shippable demo). **Mid-level** is what makes the same app interview-credible for mid roles.

### Phase 1 — Ship v1 (lab / junior bar)

- [x] Dashboard skeleton: hero + five cards + dark CSS (`Index.cshtml` + `site.css`) — **2026-07-20**
- [x] Facilitator Slide One notes kept local (gitignored) — **2026-07-20**
- [ ] All five cards working (success **or** clear error — never blank)
- [ ] `IHttpClientFactory` / `AddHttpClient<T>` with ~10s timeouts
- [ ] Services separated from `Index.cshtml.cs`
- [ ] Teaching comments stripped from public UI files
- [ ] Deployed to Azure App Service; live URL filled in above
- [ ] Screenshots linked for Weather → shipped board

### Phase 2 — Must-haves for mid-level

1. **API integration done properly**
   - [ ] Typed models (not raw JSON blobs in the page)
   - [ ] Timeouts, retries with backoff, graceful per-card failure
   - [ ] Config + secrets for any keys (never hardcoded)

2. **Caching & performance**
   - [ ] In-memory (or distributed) cache with clear TTLs  
     (e.g. weather 10m · HN 5m · crypto 1m)
   - [ ] Document why those TTLs exist (cost / rate limits / freshness)

3. **Architecture past one page**
   - [ ] One service per API (`IWeatherService`, etc.)
   - [ ] Options pattern for config
   - [ ] Shared result type (`CardResult<T>` / `ApiResult<T>`)

4. **Resilience & observability**
   - [ ] Per-card error UI (“GitHub unavailable”)
   - [ ] Structured logging on failures
   - [ ] Health checks (`/health`)

5. **Data layer**
   - [ ] EF Core + SQL (SQLite OK; Postgres/Azure SQL stronger)
   - [ ] One real persisted feature (favorites, prefs, or challenge history)
   - [ ] Migrations checked in

6. **Auth**
   - [ ] Sign-in (ASP.NET Identity or GitHub OAuth)
   - [ ] At least one personalized or protected surface

7. **Tests**
   - [ ] Unit tests for services (mock `HttpMessageHandler`)
   - [ ] A few integration tests for pages or endpoints

8. **CI/CD + deploy**
   - [ ] GitHub Actions: build + test
   - [ ] Deploy pipeline to Azure
   - [ ] README: architecture, env vars, how to run

### Phase 3 — Strong upgrades (pick 2–3)

- [ ] Background refresh (`IHostedService` / Hangfire)
- [ ] Polly resilience policies
- [ ] Docker (+ compose for local DB)
- [ ] OpenAPI / Swagger for any JSON endpoints
- [ ] Feature flags or config toggles
- [ ] Partial crypto update (no full-page meta-refresh)
- [ ] Stronger mobile CSS

### Skip (unless extra time)

- More API cards with the same pattern
- UI frameworks that don’t add backend depth
- Microservices for a portfolio dashboard

### Mid-level demo story (target)

> DevPulse is an ASP.NET Core dashboard that aggregates live developer signals. It uses typed HttpClient services, caching, per-source failure isolation, EF Core for user prefs, GitHub OAuth, health checks, automated tests, and is deployed to Azure with CI.

Be ready to answer: *What happens when Hacker News is down?* and *How do you avoid rate limits?*

---

## Lab build guide

**Tech Affiliates lab · ASP.NET Core Razor Pages · .NET 8 · C#**

The section below is the typed walkthrough for the **v1 live session**. **You** create the files and type the code.  
Do **not** ask an AI to “just finish the app” — that skips the learning.

After v1 ships, follow **Phase 2–3** above to push the same repo to a mid-level portfolio bar.

**v1 lab rules:** no database yet, no API keys required, beginner-readable C#, services separate from the page UI.  
**Deploy target:** Azure App Service (Step 7).

Facilitator slide decks and speaker notes are **not** in this repo (see `.gitignore`). Students use this README as the typed walkthrough.

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
dotnet run
```

You should see **five empty cards** on a dark themed page.

**Status (2026-07-20):** Step 1 complete in-repo — dark theme + five cards.

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

Put that URL at the **top of this README** under **Live demo**.

**Screenshot:** `screenshots/07-shipped.png`

---

## Architecture (what “good” looks like)

### Now (after 2026-07-20)

```text
Browser
  → Pages/Index.cshtml  (hero + five placeholder cards)
  → wwwroot/css/site.css  (dark theme · grid · card chrome)
  → Index.cshtml.cs       (empty OnGet — APIs not wired yet)
```

### v1 (lab target)

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

### Mid-level target

```text
Browser
  → Auth (Identity / OAuth)
  → Pages/Index (+ prefs)
       → cached services (IHttpClientFactory + TTL)
            → upstream APIs
       → EF Core (favorites / prefs / challenge history)
  → /health
  → CI: GitHub Actions → Azure
```

---

## Concepts you can put on a resume / LinkedIn

**Right now (after skeleton session — 2026-07-20)**
- ASP.NET Core Razor Pages layout (hero + multi-card dashboard shell)  
- Separation of concerns: Razor markup vs `wwwroot` static CSS  
- CSS Grid layout (`grid-template-columns`, gap, max-width centering) for a dark dashboard UI  

**After v1**
- ASP.NET Core Razor Pages  
- C# `HttpClient` and `async`/`await`  
- JSON deserialization to models  
- Parallel requests with `Task.WhenAll`  
- Dependency injection basics  
- Deploying .NET to Azure  
- Live facilitation / build-in-public delivery  

**After mid-level phases**
- `IHttpClientFactory`, caching, and rate-limit-aware design  
- Resilience (timeouts, retries, per-dependency failure isolation)  
- EF Core + migrations  
- Auth (Identity or OAuth)  
- Automated tests + GitHub Actions CI/CD  
- Health checks and structured logging  

Skeleton one-liner: *Scaffolded a dark ASP.NET Core Razor Pages dashboard shell with a five-card CSS Grid layout, ready for typed HttpClient API services.*

v1 one-liner: *Built and shipped a multi-API Razor Pages dashboard in C# (.NET 8), integrating five public APIs with dependency injection, async HTTP, and Azure App Service — facilitated as a 4-hour live build.*

Mid-level one-liner: *Built a production-minded ASP.NET Core dashboard that aggregates live developer signals via typed HttpClient services, caching, per-source failure isolation, EF Core prefs, auth, automated tests, and Azure CI/CD.*

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

## License / credit

Tech Affiliates · Build in public · Greenville NC
