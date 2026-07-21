# Slide 3 — Remaining cards & Ship

**Status:** Upcoming  
**Slides:** _Google Slides link coming soon_  
**Repo:** [github.com/kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse) · **Site:** [kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)

> **Goal:** Finish **Hacker News · GitHub · Crypto · Daily Challenge** by copying the Weather wiring from Slide 2, then polish and ship to Azure.

**Prev:** [Slide 2 — Live API Cards (Weather)](../slide-02-live-api-cards/README.md) · [← README](../../README.md)

**Prerequisite:** Weather card works ([Slide 2 checkpoint](../slide-02-live-api-cards/README.md#slide-2-checkpoint)).

**How to use this guide:**
- **HTTP cards (HN · GitHub · Crypto)** = same shape as Weather — click the Slide 2 links when you need the typed walkthrough.
- **Challenge** = different pattern (no HTTP) — skip `AddHttpClient`.
- **Then** polish + Azure.

Paths stay the same: `Models/` · `Services/` at project root. Namespaces: `DevPulse.Models` / `DevPulse.Services`.

---

## Repeat for every HTTP card (HN · GitHub · Crypto)

**Open your Weather files and copy the same shape.** Do not invent a new architecture.

### Back-map — click when stuck

| You are doing… | Copy the pattern from… | Slide 2 § |
|---|---|---|
| New model (properties only) | `WeatherInfo.cs` | [**2B**](../slide-02-live-api-cards/README.md#2b) |
| Shared wrapper (already done) | `CardResult.cs` — reuse it | [**2A**](../slide-02-live-api-cards/README.md#2a) |
| Service: field → ctor → GetAsync → try/catch | `WeatherService.cs` | [**2C**](../slide-02-live-api-cards/README.md#2c) · [C1](../slide-02-live-api-cards/README.md#2c1) · [C2](../slide-02-live-api-cards/README.md#2c2) · [C3](../slide-02-live-api-cards/README.md#2c3) |
| Register typed HttpClient | `Program.cs` | [**2D**](../slide-02-live-api-cards/README.md#2d) |
| Inject + property + `OnGetAsync` | `Index.cshtml.cs` | [**2E**](../slide-02-live-api-cards/README.md#2e) |
| `@if (….Ok)` / error | Weather article in `Index.cshtml` | [**2F**](../slide-02-live-api-cards/README.md#2f) |

### Recipe — same 5 steps every time

Swap names for HN / GitHub / Crypto. Click the Slide 2 links when you need the typed walkthrough.

1. **Model** — [§2B](../slide-02-live-api-cards/README.md#2b)  
   Add a new file under `Models/` for this card’s shape. Give it the properties the UI needs (get/set only). No HTTP here. Reuse [`CardResult<T>`](../slide-02-live-api-cards/README.md#2a) — do **not** invent a new wrapper.

2. **Service** — [§2C](../slide-02-live-api-cards/README.md#2c) (copy `WeatherService`)  
   Same skeleton: a place to store the client ([field](../slide-02-live-api-cards/README.md#2c1)) → a constructor that receives the client and fills that slot ([ctor](../slide-02-live-api-cards/README.md#2c2)) → one async method that returns `CardResult<YourModel>` ([Get…Async](../slide-02-live-api-cards/README.md#2c3)).  
   Inside the method:
   - Put the API address in a variable.
   - Ask your stored client for that address and wait for the reply; keep the reply in a local name you can read from.
   - Fail fast if the reply isn’t a success.
   - Open the body, parse JSON, pull out the fields you need, and fill your model.
   - Success door → put the model in `Data`. Error door → put a clear message in `Error`.

3. **DI** — [§2D](../slide-02-live-api-cards/README.md#2d)  
   In `Program.cs`, register this service the same way Weather is registered (typed `HttpClient`, short timeout). Add a **new** line next to Weather’s — do not delete or replace Weather’s registration.

4. **Page model** — [§2E](../slide-02-live-api-cards/README.md#2e)  
   In `Index.cshtml.cs`: accept the new service (field + constructor parameter), expose a public card property (`CardResult<…>?`), and in `OnGetAsync` **add another await** that fills that property. Leave Weather’s await in place — the page grows; it doesn’t swap.

5. **Razor** — [§2F](../slide-02-live-api-cards/README.md#2f)  
   In that card’s `<article>`, remove the muted “coming soon” text. If the card result is ok, show `.Data`; otherwise show `.Error`. List cards: loop over `.Data` and render each item.

**Stuck?** Diff against `Services/WeatherService.cs` (field → constructor → GetAsync → try/catch → `CardResult`). For wiring, diff your Weather lines in `Program.cs` and `Index.cshtml.cs`.

---

## <a id="hacker-news"></a>Hacker News (Task.WhenAll)

### Goal
Top 5 HN story titles (with links) on the `#hn` card.

### Same as Weather (do these first — click back)

| Step | What to do | Go back to |
|---|---|---|
| 1 | Create `Models/NewsStory.cs` with `Title`, `Url` (like `WeatherInfo`) | [§2B](../slide-02-live-api-cards/README.md#2b) |
| 2 | Create `Services/HackerNewsService.cs` — same field / ctor / `Get…Async` / `CardResult` shape | [§2C](../slide-02-live-api-cards/README.md#2c) |
| 3 | `AddHttpClient<…HackerNewsService>` next to Weather | [§2D](../slide-02-live-api-cards/README.md#2d) |
| 4 | Inject service; add `CardResult<List<NewsStory>>? HackerNews`; await in `OnGetAsync` | [§2E](../slide-02-live-api-cards/README.md#2e) |
| 5 | In `<article id="hn">`, replace muted text with Ok / Error | [§2F](../slide-02-live-api-cards/README.md#2f) |

### Only difference (this card’s twist)
Weather = **one** GET → one object.  
HN = **one** GET for IDs, then **five** GETs for items — prefer `Task.WhenAll`.

**Return type:** `Task<CardResult<List<NewsStory>>>`  
(`CardResult` still wraps the whole card — `T` is a list. Reuse [§2A](../slide-02-live-api-cards/README.md#2a); do not invent a new wrapper.)

**API URLs**
- `https://hacker-news.firebaseio.com/v0/topstories.json`
- `https://hacker-news.firebaseio.com/v0/item/{id}.json`

**In the service** (after the Weather-shaped try block from [§2C3](../slide-02-live-api-cards/README.md#2c3) is in place):
1. GET topstories → parse JSON array of ints  
2. Take first **5** IDs  
3. For each id: GET item → map `title` / `url`  
4. If item has no `url`, use `https://news.ycombinator.com/item?id={id}`  
5. `Data =` the list of `NewsStory`

**Razor** (only UI change vs [§2F](../slide-02-live-api-cards/README.md#2f)): when `Ok`, `@foreach (var story in Model.HackerNews.Data!)` and render links — same Ok / Error doors as Weather.

### Checkpoint
Five HN headlines appear (or a clear error on the card).

---

## <a id="github-trending"></a>GitHub Trending

### Goal
Repos created in the last week, sorted by stars, on `#github`.

### Same as Weather

| Step | What to do | Go back to |
|---|---|---|
| 1 | `Models/GitHubRepo.cs`: `Name`, `Url`, `Stars` | [§2B](../slide-02-live-api-cards/README.md#2b) |
| 2 | `Services/GitHubService.cs` — mirror WeatherService | [§2C](../slide-02-live-api-cards/README.md#2c) |
| 3 | `AddHttpClient<…GitHubService>` (+ User-Agent — see below) | [§2D](../slide-02-live-api-cards/README.md#2d) |
| 4 | Property `CardResult<List<GitHubRepo>>? GitHub` + await in `OnGetAsync` | [§2E](../slide-02-live-api-cards/README.md#2e) |
| 5 | Ok / `@foreach` / Error in `#github` | [§2F](../slide-02-live-api-cards/README.md#2f) |

### Only difference
1. **Date in the URL:** `{yyyy-MM-dd}` = UTC today minus 7 days  
   `https://api.github.com/search/repositories?q=created:>{date}&sort=stars&order=desc&per_page=5`
2. **User-Agent required** — set on the `HttpClient` in the `AddHttpClient` callback ([§2D](../slide-02-live-api-cards/README.md#2d)), e.g. `client.DefaultRequestHeaders.UserAgent.ParseAdd("DevPulse");`
3. **JSON path:** `items[]` → `full_name` → `Name`, `html_url` → `Url`, `stargazers_count` → `Stars`  
   (Same idea as Open-Meteo’s `current.temperature_2m` → `TemperatureC` in [§2C3](../slide-02-live-api-cards/README.md#2c3).)

### Checkpoint
Repos + star counts, or a clear rate-limit / error message on the card.

---

## <a id="crypto"></a>Crypto (auto-refresh)

### Goal
BTC + ETH USD prices on `#crypto`; page reloads ~ every 60s.

### Same as Weather

| Step | What to do | Go back to |
|---|---|---|
| 1 | `Models/CryptoPrices.cs`: `BtcUsd`, `EthUsd` | [§2B](../slide-02-live-api-cards/README.md#2b) |
| 2 | `Services/CryptoService.cs` — one GET, map JSON, `CardResult<CryptoPrices>` | [§2C](../slide-02-live-api-cards/README.md#2c) (closest to Weather — single call) |
| 3 | `AddHttpClient<…CryptoService>` | [§2D](../slide-02-live-api-cards/README.md#2d) |
| 4 | Property + await in `OnGetAsync` | [§2E](../slide-02-live-api-cards/README.md#2e) |
| 5 | Ok / Error in `#crypto` | [§2F](../slide-02-live-api-cards/README.md#2f) |

### Only difference (not in Weather)
Meta refresh so the browser reloads and `OnGetAsync` runs again:

1. Top of `Index.cshtml`:
```cshtml
@section Head {
    <meta http-equiv="refresh" content="60" />
}
```
2. In `Pages/Shared/_Layout.cshtml` inside `<head>`:
```cshtml
@await RenderSectionAsync("Head", required: false)
```

**API:** `https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd`  
Map `bitcoin.usd` / `ethereum.usd` (same `GetProperty` style as [§2C3](../slide-02-live-api-cards/README.md#2c3)).

### Checkpoint
Prices show; full page refresh ~ every 60s.

---

## <a id="daily-challenge"></a>Daily Challenge (no API — different pattern)

### Goal
Button picks a random challenge from a local list on `#challenge`.

### What to skip
Do **not** copy [§2C](../slide-02-live-api-cards/README.md#2c) `HttpClient`, [§2D](../slide-02-live-api-cards/README.md#2d) `AddHttpClient`, or `CardResult`. This card is local + POST.

### What to do (reuse Index-twin ideas)

| Step | What to do | Related Slide 2 § (concept only) |
|---|---|---|
| 1 | `Services/ChallengeService.cs` — `string[]` + `PickRandom()` | Logic in Services, not Models |
| 2 | `Program.cs`: `builder.Services.AddSingleton<DevPulse.Services.ChallengeService>();` | Like [§2D](../slide-02-live-api-cards/README.md#2d), but **Singleton**, not HttpClient |
| 3 | `Index.cshtml.cs`: field + ctor; `string? Challenge`; read `TempData` on GET; add POST handler below | [§2E](../slide-02-live-api-cards/README.md#2e) (inject + property) + a POST handler Weather never had |
| 4 | `#challenge`: show `Challenge`; form posts to handler | [§2F](../slide-02-live-api-cards/README.md#2f) (replace muted text) |

**POST handler (required):**

```csharp
public IActionResult OnPostNewChallenge()
{
    TempData["Challenge"] = _challenges.PickRandom();
    return RedirectToPage();
}
```

**Razor:**

```cshtml
<p>@(Model.Challenge ?? "Click for a challenge.")</p>
<form method="post" asp-page-handler="NewChallenge">
    <button type="submit">New challenge</button>
</form>
```

### Checkpoint
Button shows a new challenge string (no external API).

---

## Cards checkpoint (before ship)

- [ ] Hacker News shows top 5 titles
- [ ] GitHub Trending shows repos + stars
- [ ] Crypto shows BTC/ETH
- [ ] Daily Challenge form works locally

---

## Polish

### Step 6.5 — Polish (portfolio bar)

Do these before you call the project “done”:

1. **Every card** has success UI **or** a clear `.error` message (never blank).  
2. **Timeouts** on all `HttpClient` registrations (10s is fine) — same idea as [§2D](../slide-02-live-api-cards/README.md#2d).  
3. **README live URL** placeholder filled after Azure deploy.  
4. Optional: last-updated time: `@DateTime.Now.ToString("t")` in the hero.  
5. Optional: trim Privacy nav link if you do not need it.

---

## Ship to Azure

### Step 7 — Ship to Azure

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
### Login and follow Azure docs for your subscription
az login
### Create resource group + webapp, then deploy
### See: https://learn.microsoft.com/azure/app-service/quickstart-dotnetcore
```

### Checkpoint
Open the Azure URL on your phone → all five cards load.

Put that URL at the **top of the root README** under **Live demo**.

---

## What you can say you learned

- What an API is  
- How `HttpClient` requests data in C#  
- Mapping JSON to models  
- Why `async` / `await` matters (including `Task.WhenAll`)  
- Razor Pages (server-rendered HTML)  
- Light dependency injection  
- Guarding failures with `try` / `catch`  
- Deploying ASP.NET to Azure  

**Resume line:** *Built and deployed a multi-API Razor Pages dashboard in C# (.NET 8).*

---

## Slide 3 checkpoint

- [ ] All five cards work locally (Weather from Slide 2 + four above)
- [ ] Live HTTPS URL opens on a phone
- [ ] Demo URL filled into the root README
