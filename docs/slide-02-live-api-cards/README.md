# Slide 2 — Live API Cards

**Status:** Done  
**Slides:** _Google Slides link coming soon_ · **Facilitator notes:** [notes.md](./notes.md)  
**Repo:** [github.com/kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse) · **Site:** [kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)

> **Goal:** Wire the **Weather** card end-to-end — typed `HttpClient` service, models, DI, and per-card errors. Layout from Slide 1 stays; this slide teaches the pattern the other cards will copy.

**Prev:** [Slide 1 — Dashboard Skeleton](../slide-01-dashboard-skeleton/README.md) · **Next:** [Slide 3 — Remaining cards & Ship](../slide-03-ship-and-close/README.md) · [← README](../../README.md)

**How to use this guide:**
- **Weather** = full walkthrough (images + syntax + type-along) — sections **2A–2F** below.
- **HN / GitHub / Crypto / Challenge** → [Slide 3](../slide-03-ship-and-close/README.md) (same wiring; click back to **2A–2F** when stuck).

**Paths:** Create folders at the **project root** (same level as `Pages/`, `Program.cs`, `wwwroot/`). Namespaces: `DevPulse.Models` / `DevPulse.Services`.

### Models vs Services (say this out loud)

| Folder | Put here | Do NOT put here |
|---|---|---|
| `Models/` | Data shapes (`WeatherInfo`, …) + shared `CardResult<T>` | `HttpClient` calls, URLs, parsing |
| `Services/` | Fetch + map JSON into models | Razor markup / page UI |

**One-liner:** Models describe. Services fetch. Pages show.

**Cheat map (keep this in mind for the whole Weather path):**

![Weather — which file for which line](images/weather-files-map.jpg)

---

## Weather (first HttpClient)

### Goal
Show real weather data on the Weather card.

### Why
First full path: **HTTP → JSON → C# model → page property → Razor**.

---

### <a id="2a"></a>2A — Models: shared result wrapper

**Big picture first** — Models hold data; they do **not** call the API:

![How Models fit the data flow](images/modeldataflowexplanation.png)

Create folder `Models/` at project root (sibling of `Pages/` — **not** inside `wwwroot`):

![Create Models folder](images/createdmodelsfolder.png)

Create `Models/CardResult.cs`:

![Create CardResult.cs](images/createdcardresultscsfile.png)

**Teaching — generics (`T`):** one reusable box, many fillings:

![Generics: one box design](images/generic_box_analogy.png)

Same idea as a car body + engine bay (`T`). This is **composition**, not inheritance:

![Generics: car and engine bay](images/genericautomotiveimage.png)

Type:

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
| `namespace DevPulse.Models` | Address book for model types |
| `CardResult<T>` | Generic wrapper — `T` is the payload type |
| `T?` / `string?` | May be null |
| `Ok => ...` | Computed property (PascalCase — Razor will use `.Ok`) |

> **Casing matters:** `Ok` not `ok`. C# is case-sensitive.

![C# is case-sensitive](images/csharp-case-sensitive.jpg)

---

### <a id="2b"></a>2B — Models: weather payload

Create `Models/WeatherInfo.cs`:

![Create WeatherInfo.cs](images/createdweatherinfocsfile.png)

**Generic wrapper vs concrete payload:**

![Generic vs concrete](images/generic_vs_concrete_class.png)

Type:

```csharp
namespace DevPulse.Models;

public class WeatherInfo
{
    public double TemperatureC { get; set; }
    public double WindKph { get; set; }
    public string Summary { get; set; } = "";
}
```

| Syntax | Meaning |
|---|---|
| `{ get; set; }` | Readable + writable property |
| `WindKph` | Must match exactly what the service assigns later |
| `= ""` | Safe empty string so Razor never hits null on `Summary` |

**Why Models before the Service?** Destination before journey — the service needs a shape to fill:

![Destination before journey](images/modelsbeforeservice.jpg)

---

### <a id="2c"></a>2C — WeatherService (fetch + map)

Starting point: Models exist; Services not yet:

![Models exist · Services not yet](images/a0f06680-263e-497e-b7c7-e08001a86514.jpg)

Create `Services/` at project root, then `WeatherService.cs`:

![Create Services folder](images/createservicesfolder.png)

![Services/WeatherService.cs created](images/services-weatherservicecsimage.jpg)

**Why `System.Text.Json`?** Built into .NET — no NuGet for this lab:

![When to use System.Text.Json](images/when_to_use_system_text_json_v2.png)

The two `using` lines “reel in” a framework tool and your models:

![using statements analogy](images/systemtextjsonusingstatementsanalogy.png)

**Trap:** never put `HttpClient` calls in a Model. Shape ≠ Fetch:

![Wrong class — HttpClient in Model](images/puttinghttpclientcallsinthemodel.jpg)

#### <a id="2c1"></a>Step C1 — Field (slot + rule)

```csharp
private readonly HttpClient _httpClient;
```

| Syntax | Meaning |
|---|---|
| `private` | Only this class can touch it |
| `readonly` | Assign once (in the constructor) |
| `HttpClient` | **Type** that can send HTTP |
| `_httpClient` | **Field name** (storage) |

![Field first — slot + readonly](images/field-slot-and-readonly.jpg)

`readonly` is a **rule**, not proof the constructor already ran:

![readonly is a rule, not a timeline](images/readonly-rule-vs-constructor.jpg)

In the file, the field is **above**; the constructor that fills it is **below**:

![Constructor comes after](images/constructor-comes-after.jpg)

#### <a id="2c2"></a>Step C2 — Constructor (HttpClient handed in)

```csharp
public WeatherService(HttpClient httpClient)
{
    _httpClient = httpClient;
}
```

| Syntax | Meaning |
|---|---|
| Same name as class, no return type | This is a **constructor**, not a method |
| `(HttpClient httpClient)` | Parameter — value being handed in |
| `_httpClient = httpClient` | Store it in the field |

![Constructor — HttpClient handed in](images/constructor-DI.jpg)

![How HttpClient gets into WeatherService](images/http-to-weather-service.jpg)

![Parameter vs field names](images/parameterandstorageweatherservice.jpg)

![Common HttpClient mix-ups](images/_common_mixups_httpclient.jpg)

#### <a id="2c3"></a>Step C3 — Method `GetAsync` (order placed → map → two doors)

```csharp
public async Task<CardResult<WeatherInfo>> GetAsync()
```

| Syntax | Meaning |
|---|---|
| Has a return type | This is a **method**, not a constructor |
| `async Task<...>` | Can `await` network work; returns a promise of `CardResult<WeatherInfo>` |
| `CardResult<WeatherInfo>` | Wrapper holding weather **or** an error — **not** inheritance |

**Full flow map (Task → try → catch):** [WeatherService Data Flow (PDF)](./images/WeatherService%20Data%20Flow.pdf)

![CardResult is NOT inheritance](images/cardresultisnotinheritance.jpg)

![Inheritance vs generics](images/genericsvsinheritance.jpg)

Inside the method — **order placed**:

```csharp
var url =
    "https://api.open-meteo.com/v1/forecast?latitude=35.61&longitude=-77.37&current=temperature_2m,wind_speed_10m";

using var response = await _httpClient.GetAsync(url);
response.EnsureSuccessStatusCode();
```

![Order placed — Open-Meteo GetAsync](images/open-meteo-order-placed.jpg)

**Map JSON → model** (API names → your property names):

```csharp
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
```

| Syntax | Meaning |
|---|---|
| `temperature_2m` | API’s JSON field name |
| `TemperatureC` | Your C# property name |
| `Data = new WeatherInfo { ... }` | Put the payload inside the wrapper |

Follow the temperature across files (always via `.Data`):

![Follow TemperatureC / .Data path](images/data-property-path.jpg)

**Two doors** — wrap the body in `try` / `catch`:

```csharp
catch (Exception ex)
{
    return new CardResult<WeatherInfo>
    {
        Error = $"Couldn't load weather: {ex.Message}"
    };
}
```

![One method · two doors](images/try-catch-two-doors.jpg)

Same path as a printable diagram: [WeatherService Data Flow (PDF)](./images/WeatherService%20Data%20Flow.pdf)

Full service reference (type this into `Services/WeatherService.cs`):

```csharp
using System.Text.Json;
using DevPulse.Models;

namespace DevPulse.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CardResult<WeatherInfo>> GetAsync()
    {
        try
        {
            var url =
                "https://api.open-meteo.com/v1/forecast?latitude=35.61&longitude=-77.37&current=temperature_2m,wind_speed_10m";

            using var response = await _httpClient.GetAsync(url);
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
            return new CardResult<WeatherInfo>
            {
                Error = $"Couldn't load weather: {ex.Message}"
            };
        }
    }
}
```

**Checkpoint — two folders, two jobs — next is DI:**

![Models + Services ready for DI](images/modelsandservicereadyforDI.jpg)

---

### <a id="2d"></a>2D — Register in `Program.cs` (dependency injection)

**Right after** `builder.Services.AddRazorPages();`:

```csharp
builder.Services.AddHttpClient<DevPulse.Services.WeatherService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
```

| Syntax | Meaning |
|---|---|
| `AddHttpClient<WeatherService>` | DI creates `WeatherService` and hands it an `HttpClient` |
| `Timeout` | Don’t hang forever if the API is slow |

![Program.cs — AddHttpClient](images/program-addhttpclient.jpg)

![IDE: Program.cs updated](images/programcsupdatedwithDevPulseService.png)

![DI wiring lines](images/di-wiring-lines.jpg)

---

### <a id="2e"></a>2E — Page model (`Index.cshtml.cs`)

**Index has TWO files** — don’t mix them up:

![Index twins — .cshtml.cs vs .cshtml](images/index-twins-cshtml-vs-cs.jpg)

![Explorer: both Index files](images/explorer-both-index-files.jpg)

| File | Job |
|---|---|
| `Index.cshtml.cs` | Logic — inject services, call APIs, set properties |
| `Index.cshtml` | UI — HTML cards; read `Model.…` |

Open `Pages/Index.cshtml.cs` and wire Weather (same field → constructor → property → method pattern):

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
| `IndexModel : PageModel` | **Inheritance** — this **is a** Razor page model |
| `private readonly WeatherService _weather` | Field for the service |
| `IndexModel(WeatherService weather)` | DI hands in the service |
| `OnGetAsync` | Runs when the browser GETs `/` |
| `Weather = await _weather.GetAsync()` | Store the `CardResult` for the view |

![OnGetAsync fills it · Razor shows it](images/ongetasync-to-razor.jpg)

![Request path — line to line](images/request-path-lines.jpg)

---

### <a id="2f"></a>2F — Razor card (`Index.cshtml`)

In the **Weather** `<article id="weather">`, replace the muted “coming soon” paragraph:

![Replace muted paragraph in Weather article](images/replace-p-muted-weather-article.png)

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
| `Model.Weather` | Property from `Index.cshtml.cs` |
| `?.` | Safe if null |
| `Ok == true` | Wrapper says success |
| `Data!.Summary` | Read the string on `WeatherInfo` **inside** the wrapper |
| `else` + `Error` | Friendly failure text |

### Checkpoint
`dotnet run` → Weather card shows a temperature line (or a clear error):

![Weather card completed](images/weathercard_completed.png)

---

## <a id="slide-2-checkpoint"></a>Slide 2 checkpoint

- [ ] Weather shows temperature (or a clear error)

**When finished →** [Slide 3 — Remaining cards & Ship](../slide-03-ship-and-close/README.md) (HN · GitHub · Crypto · Challenge, then Azure)
