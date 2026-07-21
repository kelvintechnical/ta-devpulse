# DevPulse — Slide Two PPT Notes

**Location:** `docs/slide-02-live-api-cards/`  
**Deck focus:** Live API cards — Models → Services → Page Model → Razor (Weather first)  
**Lab guide:** [README.md](./README.md) (embeds the same images for GitHub readers)  
**Practice Qs:** [questions.md](./questions.md)  
**Source files:** `Models/CardResult.cs`, `Models/WeatherInfo.cs`, `Services/WeatherService.cs`, `Program.cs`, `Pages/Index.cshtml.cs`, `Pages/Index.cshtml`  
**Visual refs:** `images/`  
**Repo layout:** App files live at the **repo root** (`Models/`, `Services/`, `Pages/`, `Program.cs`) — same level as `docs/`.

---

## Progress (as of 2026-07-20)

| Area | Status |
|---|---|
| Slide 1 — skeleton + dark CSS | Done |
| Models folder + `CardResult` / `WeatherInfo` teaching images | Ready |
| Generics teaching (box, car, generic vs concrete) | Ready |
| Models data-flow diagram | Ready |
| Services folder screenshot | Ready |
| `System.Text.Json` decision + `using` analogy | Ready |
| Filled `WeatherService` + DI + page bind screenshots | **Not yet** |
| Browser Weather card live screenshot | **Not yet** |
| HN / GitHub / Crypto / Challenge | Not started |

**You are here:** Models complete → start **2C WeatherService**.  
**PPT build:** Use this file + `images/` the same way Slide One used `facilitator/slides/slide-one/notes.md`.

---

## Image inventory (PPT + README)

| File | What it shows | PPT slide | README section |
|---|---|---|---|
| `images/modeldataflowexplanation.png` | API → Service → **Models** → Page Model → Razor | Opening / anchor | Before 2A |
| `images/createdmodelsfolder.png` | Explorer: create `Models/` (watch for wrong `wwwroot` parent) | 1 | 2A |
| `images/createdcardresultscsfile.png` | Explorer: `Models/CardResult.cs` | 2 | 2A |
| `images/createdweatherinfocsfile.png` | Explorer: `WeatherInfo.cs` + `CardResult.cs` | 3 | 2B |
| `images/generic_box_analogy.png` | Generics: one box design, many fillings | 4 | 2A teach |
| `images/genericautomotiveimage.png` | Generics: car body + engine bay (`T`) | 5 | 2A teach |
| `images/generic_vs_concrete_class.png` | `CardResult<T>` wrapper vs `WeatherInfo` payload | 6 | 2B teach |
| `images/createservicesfolder.png` | Explorer: create `Services/` | 7 | 2C |
| `images/when_to_use_system_text_json_v2.png` | When to use `System.Text.Json` vs Newtonsoft | 8 | 2C |
| `images/systemtextjsonusingstatementsanalogy.png` | `using` statements = casting lines for tools/models | 9 | 2C |

**Duplicate (do not use in PPT):** `images/genericvsconcreteclass.png` — prefer `generic_vs_concrete_class.png`.

**Missing screenshots (add later):**
- IDE: filled `WeatherService.cs`
- IDE: `Program.cs` `AddHttpClient`
- IDE: `Index.cshtml.cs` `OnGetAsync`
- Browser: Weather card with live temp (or clear error)

---

## Folder placement rule (say early)

| Place | Correct? | Why |
|---|---|---|
| `Models/` next to `Pages/` (project root) | **Yes** | C# types compiled into the app |
| `Services/` next to `Models/` | **Yes** | Behavior / API callers |
| `wwwroot/Models/` | **No** | `wwwroot` is public static files only |

If `createdmodelsfolder.png` shows `Models` under `wwwroot`, narrate the fix: move it to project root before adding `.cs` files.

---

## IDE syntax colors (match VS Code Dark+ on every code slide)

| Token | Color | Hex |
|---|---|---|
| Background | Deep charcoal | `#1E1E1E` |
| Plain text | Off-white | `#D4D4D4` |
| Keywords (`namespace`, `public`, `class`, `bool`, `async`, `await`) | Blue | `#569CD6` |
| Types (`CardResult`, `WeatherInfo`, `HttpClient`, `string`) | Teal | `#4EC9B0` |
| Generics (`<T>`) | Off-white / gold | `#D4D4D4` / `#DCDCAA` |
| Properties / locals | Light blue | `#9CDCFE` |
| Strings | Salmon | `#CE9178` |
| Comments (`//`) | Olive green | `#6A9955` |
| Operators (`=>`, `is`, `&&`) | Gray / white | `#D4D4D4` |

---

## Slide index

(slide 0), <!-- Big picture data flow — modeldataflowexplanation -->
(slide 1), <!-- Create Models/ — createdmodelsfolder -->
(slide 2), <!-- Create CardResult.cs — createdcardresultscsfile -->
(slide 3), <!-- Generics box — generic_box_analogy -->
(slide 4), <!-- Generics car — genericautomotiveimage -->
(slide 5), <!-- Type CardResult.cs code -->
(slide 6), <!-- Create WeatherInfo.cs — createdweatherinfocsfile -->
(slide 7), <!-- Generic vs concrete — generic_vs_concrete_class -->
(slide 8), <!-- Type WeatherInfo.cs code -->
(slide 9), <!-- Models checkpoint -->
(slide 10), <!-- Create Services/ — createservicesfolder -->
(slide 11), <!-- Why System.Text.Json — when_to_use_system_text_json_v2 -->
(slide 12), <!-- using statements — systemtextjsonusingstatementsanalogy -->
(slide 13), <!-- Type WeatherService.cs (code; screenshot TBD) -->
(slide 14), <!-- DI in Program.cs (TBD) -->
(slide 15), <!-- Index.cshtml.cs + Index.cshtml (TBD) -->
(slide 16), <!-- Checkpoint: dotnet run Weather card (TBD) -->

---

<!-- (slide 0) -->

## Slide 0 — How Models fit the data flow

**On slide:** Full architecture diagram  
**Image:** `images/modeldataflowexplanation.png`  
**Goal:** Show the whole Weather path before typing files.

**Walk left → right:**
1. **External API** — Open-Meteo · JSON over HTTP  
2. **Service** — `Services/WeatherService.cs` · `HttpClient`  
3. **Models** (highlight) — `WeatherInfo` + `CardResult<T>` → `CardResult<WeatherInfo>`  
4. **Page model** — `Pages/Index.cshtml.cs` · `OnGetAsync`  
5. **Razor** — `Pages/Index.cshtml` · Weather card  

**Speaker notes:**
- Banner: `Program.cs` registers `WeatherService` with DI (`AddHttpClient`).
- Dashed rule: **Models do NOT call the API; they only hold typed data.**
- Lab one-liner: **HTTP → JSON → C# models → Razor.**
- Mental model: API = waiter · `HttpClient` = order · JSON = plate · model = labels · Razor = food on the table.

**Checkpoint:** Student can point to Models and say “data shapes only.”

---

<!-- (slide 1) -->

## Slide 1 — Create the `Models/` folder

**Image:** `images/createdmodelsfolder.png`  
**Goal:** Home for typed data before any API call.

**Speaker notes:**
- Create **`Models/`** at project root (sibling of `Pages/`, `wwwroot/`).
- Trap: not inside `wwwroot`.
- One-liner: “Models hold data. Services fetch data. Pages show data.”

**Checkpoint:** `Models/` next to `Pages/`.

---

<!-- (slide 2) -->

## Slide 2 — Create `CardResult.cs`

**Image:** `images/createdcardresultscsfile.png`  
**Goal:** Shared success/error wrapper file exists.

**Speaker notes:**
- Inside `Models/`, create **`CardResult.cs`**.
- Will hold generic `CardResult<T>` — reused by every card later.
- File-exists beat first; generics pictures next; then type the body.

**Checkpoint:** `Models/CardResult.cs` visible.

---

<!-- (slide 3) -->

## Slide 3 — Generics: one box design

**Image:** `images/generic_box_analogy.png`  
**Goal:** Make `<T>` obvious.

**Speaker notes:**
- Left: empty `CardResult<T>` — “Contents: ____”.
- Right: `WeatherInfo` / `GitHubStats` / `int` fillings.
- One design, reused — not three separate classes.
- `T` = sticky note: “fill me in later.”

**Checkpoint:** Why we don’t need `WeatherCardResult` and `HnCardResult`.

---

<!-- (slide 4) -->

## Slide 4 — Generics: car + engine bay

**Image:** `images/genericautomotiveimage.png`  
**Goal:** Composition, not inheritance.

**Speaker notes:**
- Car body = `CardResult<T>` (Data / Error / Ok).
- Engine bay = `T`.
- **No inheritance** — car doesn’t become weather; it only checks if an engine is present.

**Checkpoint:** “Wrapper + payload,” not “WeatherInfo inherits CardResult.”

---

<!-- (slide 5) -->

## Slide 5 — Type `CardResult.cs`

**Image:** optional corner `createdcardresultscsfile.png`  
**Code (IDE colors):**

```csharp
namespace DevPulse.Models;

public class CardResult<T>
{
    public T? Data { get; set; }
    public string? Error { get; set; }
    public bool Ok => Error is null && Data is not null;
}
```

**Speaker notes:**
- `namespace` — organize types; others `using DevPulse.Models;`
- `<T>` — the blank (box / engine bay)
- `Data` / `Error` — success payload vs failure message
- `Ok` — computed; PascalCase (Razor will use `Model.Weather?.Ok`)

**Checkpoint:** Wrapper with `Data`, `Error`, `Ok`.

---

<!-- (slide 6) -->

## Slide 6 — Create `WeatherInfo.cs`

**Image:** `images/createdweatherinfocsfile.png`  
**Goal:** Concrete weather payload file next to the wrapper.

**Speaker notes:**
- Create `WeatherInfo.cs` in `Models/`.
- Payload = temp / wind / summary. Envelope = `CardResult<T>`.
- Together later: `CardResult<WeatherInfo>`.

**Checkpoint:** Both model files exist.

---

<!-- (slide 7) -->

## Slide 7 — Generic vs concrete

**Image:** `images/generic_vs_concrete_class.png`  
**Goal:** Contrast wrapper vs payload designs.

**Speaker notes:**
- Left: `CardResult<T>` — generic car body, engine bay blank on purpose.
- Right: `WeatherInfo` — concrete engine with fixed fields.
- Rule: generic when reusable across types; concrete when one known shape.
- `WeatherInfo` becomes `T` only inside `CardResult<WeatherInfo>`.

**Checkpoint:** Student can label wrapper vs payload.

---

<!-- (slide 8) -->

## Slide 8 — Type `WeatherInfo.cs`

**Image:** optional corner `createdweatherinfocsfile.png`  

```csharp
namespace DevPulse.Models;

public class WeatherInfo
{
    public double TemperatureC { get; set; }
    public double WindKph { get; set; }
    public string Summary { get; set; } = "";
}
```

**Speaker notes:**
- Not generic — fixed weather shape.
- `double` defaults to `0.0` (calm vs missing look the same — gotcha).
- `Summary = ""` — never null for Razor.
- Practice: [questions.md](./questions.md)

**Checkpoint:** `CardResult<WeatherInfo>` = envelope + this payload.

---

<!-- (slide 9) -->

## Slide 9 — Models checkpoint

**Image:** `images/modeldataflowexplanation.png` (Models panel)  

**Checklist:**
- [ ] `Models/` at project root  
- [ ] `CardResult.cs` + `WeatherInfo.cs` typed  
- [ ] Box + car + generic-vs-concrete analogies  
- [ ] Can narrate API → Service → Models → Page → Razor  

**Next:** Services beat (2C).

---

<!-- (slide 10) -->

## Slide 10 — Create the `Services/` folder

**Image:** `images/createservicesfolder.png`  
**Goal:** Place for classes that *do* work (call APIs).

**Speaker notes:**
- Create **`Services/`** at project root (sibling of `Models/`).
- Then add empty `WeatherService.cs`.
- Services fetch + map JSON → models. Models still don’t fetch.

**Checkpoint:** `Services/` visible next to `Models/`.

---

<!-- (slide 11) -->

## Slide 11 — Why `System.Text.Json`

**Image:** `images/when_to_use_system_text_json_v2.png`  
**Goal:** Justify the first `using` in `WeatherService`.

**Speaker notes:**
- Reading JSON? → need a library.
- No legacy Newtonsoft need? → **`System.Text.Json`** (built in, fast, no NuGet).
- Footer: WeatherService / Open-Meteo is the right default for the rest of DevPulse.

**Checkpoint:** Why we don’t `dotnet add package Newtonsoft.Json` for this lab.

---

<!-- (slide 12) -->

## Slide 12 — `using` statements analogy

**Image:** `images/systemtextjsonusingstatementsanalogy.png`  
**Goal:** Explain the two usings at the top of `WeatherService.cs`.

**Speaker notes:**
- Pier = `WeatherService.cs`.
- Left line → `.NET` sea → reel in `System.Text.Json`.
- Right line → project pond → reel in `DevPulse.Models`.
- Caption: “casting a line to reel in tools and models from other namespaces.”

**Checkpoint:** Student can say what each `using` unlocks.

---

<!-- (slide 13) -->

## Slide 13 — Type `WeatherService.cs`

**Image:** TBD (use `createservicesfolder.png` until filled IDE shot exists)  
**Code:** see lab README § 2C  

**Speaker notes (high level — expand line-by-line live):**
- Constructor takes `HttpClient` (DI will provide it).
- `GetAsync` returns `CardResult<WeatherInfo>`.
- `try` / `catch` — never crash the whole page for one card.
- Parse Open-Meteo JSON → fill `WeatherInfo` → wrap in `CardResult`.

**Checkpoint:** Service file saved; build may still fail until DI is registered.

---

<!-- (slides 14–16) -->

## Slides 14–16 — DI, page model, Razor, run (screenshots TBD)

| Slide | Topic | Lab § | Image status |
|---|---|---|---|
| 14 | `AddHttpClient` in `Program.cs` | 2D | TBD |
| 15 | `Index.cshtml.cs` + Weather card markup | 2E–2F | TBD |
| 16 | `dotnet run` checkpoint | Checkpoint | TBD |

Keep speaker notes short until screenshots land; pull exact code from [README.md](./README.md).

---

## Quick talk track (2 minutes)

1. Big picture: HTTP → JSON → models → Razor (`modeldataflowexplanation`).  
2. Models folder + two files.  
3. Generics three ways: box, car, generic-vs-concrete.  
4. Services folder + why `System.Text.Json` + `using` fishing analogy.  
5. Type `WeatherService` → DI → page → run.

---

## Facilitator tips

- Show **box** then **car** then **generic vs concrete** — same idea three ways beats jargon once.
- Keep data-flow diagram up while typing model properties.
- Fix `wwwroot/Models` live if it happens — high-value separation-of-concerns moment.
- Don’t rush `HttpClient` until wrapper vs payload is solid.
- After class, drop browser/IDE screenshots into `images/` and wire them into README the same way (`![alt](images/...)`).
