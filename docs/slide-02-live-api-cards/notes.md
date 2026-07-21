# DevPulse — Slide Two PPT Notes

**Location:** `docs/slide-02-live-api-cards/`  
**Deck focus:** Live API cards — Models → Services → Page Model → Razor (Weather first)  
**Lab guide:** [README.md](./README.md) (images embedded — keep inventory in sync)  
**Practice Qs (local only):** `.facilitator/questions.md` (gitignored)  
**Gemini prompts (local only):** `.facilitator/gemini-prompts-models-to-service.md` (gitignored)  
**Source files:** `Models/CardResult.cs`, `Models/WeatherInfo.cs`, `Services/WeatherService.cs`, `Program.cs`, `Pages/Index.cshtml.cs`, `Pages/Index.cshtml`  
**Visual refs:** `images/` (student-facing; committed so README renders on GitHub)  
**Repo layout:** App files live at the **repo root** (`Models/`, `Services/`, `Pages/`, `Program.cs`) — same level as `docs/`.

**PPT build:** Use this file + `images/` the same way Slide One used `facilitator/slides/slide-one/notes.md`.

---

## Progress (as of 2026-07-21)

| Area | Status |
|---|---|
| Slide 1 — skeleton + dark CSS | Done |
| **Act A — Models** | Ready (code + images + README) |
| **Act B — WeatherService** | Ready (code + images + README) |
| **Act C — DI + Index twins + Razor** | Ready (code + images + README) |
| Browser Weather card screenshot | Ready — `weathercard_completed.png` |
| HN / GitHub / Crypto / Challenge | Checklist in README (student “have at it”) |

**You are here:** Slide 2 lab guide **Done** (Weather walkthrough complete; other cards use the shared checklist). Next → Slide 3 Ship & Close.  
**Teaching spine:** Destination before journey → Shape ≠ Fetch → field/`readonly` → HttpClient handed in → order placed → two doors → Holding ≠ becoming → DI → **two Index files** → OnGetAsync → Razor → run.

---

## How this deck is organized (for PPT)

Build the deck in **three acts**. Each slide section below is one PPT slide (or one beat). Do not shuffle acts — teaching order is the point.

| Act | Lab § | Subject | Ends when… |
|---|---|---|---|
| **A — Models (destination)** | 2A–2B | Shapes only: `CardResult<T>` + `WeatherInfo` | Students can say “Models hold data; they don’t call HTTP” |
| **B — WeatherService (journey)** | 2C | Fetch + translate into those shapes | `GetAsync` returns `CardResult<WeatherInfo>`; Holding ≠ becoming |
| **C — Wire & show** | 2D–2F | DI → **Index twins** → Razor → run | Browser shows live Weather (or clear card error) |

---

## Folder placement rule (say early — Act A + Act B)

| Place | Correct? | Why |
|---|---|---|
| `Models/` next to `Pages/` (project root) | **Yes** | C# types compiled into the app |
| `Services/` next to `Models/` | **Yes** | Behavior / API callers |
| `wwwroot/Models/` or `wwwroot/Services/` | **No** | `wwwroot` is public static files only |

One-liner: **Models hold data. Services fetch data. Pages show data.**

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

## Image inventory (PPT + README)

### Act A — Models

| File | What it shows | PPT beat |
|---|---|---|
| `images/modeldataflowexplanation.png` | API → Service → **Models** → Page → Razor | A0 / A9 |
| `images/createdmodelsfolder.png` | Explorer: create `Models/` | A1 |
| `images/createdcardresultscsfile.png` | Explorer: `Models/CardResult.cs` | A2 |
| `images/generic_box_analogy.png` | Generics: one box, many fillings | A3 |
| `images/genericautomotiveimage.png` | Generics: car body + engine bay (`T`) | A4 |
| `images/createdweatherinfocsfile.png` | Explorer: both model files | A6 |
| `images/generic_vs_concrete_class.png` | Wrapper vs payload | A7 |

**Duplicate (do not use):** `images/genericvsconcreteclass.png` — prefer `generic_vs_concrete_class.png`.

### Act B — WeatherService

| File | What it shows | PPT beat |
|---|---|---|
| `images/modelsbeforeservice.jpg` | Destination before journey | B0 |
| `images/a0f06680-263e-497e-b7c7-e08001a86514.jpg` | Models exist · Services not yet | B1 |
| `images/createservicesfolder.png` | Create `Services/` (early shot) | B2 alt |
| `images/services-weatherservicecsimage.jpg` | `Services/WeatherService.cs` created | B2 |
| `images/when_to_use_system_text_json_v2.png` | Why `System.Text.Json` | B3 |
| `images/systemtextjsonusingstatementsanalogy.png` | `using` = casting lines | B4 |
| `images/puttinghttpclientcallsinthemodel.jpg` | Trap: HttpClient in Model · Shape ≠ Fetch | B5 |
| `images/field-slot-and-readonly.jpg` | Field first — slot + `readonly` | B6 |
| `images/constructor-comes-after.jpg` | Reading order — declare above, assign below | B7 |
| `images/readonly-rule-vs-constructor.jpg` | `readonly` is a rule, not a timeline | B8 |
| `images/constructor-DI.jpg` | Constructor — HttpClient handed in | B9 |
| `images/http-to-weather-service.jpg` | How HttpClient gets into WeatherService | B10 |
| `images/parameterandstorageweatherservice.jpg` | Parameter vs field names | B11 |
| `images/_common_mixups_httpclient.jpg` | Myth busters | B12 |
| `images/open-meteo-order-placed.jpg` | Order placed — Open-Meteo `GetAsync` | B13 |
| `images/try-catch-two-doors.jpg` | Success door / Error door | B15 |
| `images/cardresultisnotinheritance.jpg` | Holding ≠ becoming · read `.Data` | B16 |
| `images/genericsvsinheritance.jpg` | Inheritance (`:`) vs generics (`<>`) | B17 |
| `images/modelsandservicereadyforDI.jpg` | Two folders · two jobs · next: DI | B18 |

### Act C — Wire & show

| File | What it shows | PPT beat | README § |
|---|---|---|---|
| `images/weather-files-map.jpg` | Which line lives in which file | C0 | Top of Weather |
| `images/program-addhttpclient.jpg` | What `AddHttpClient` means | C1 | 2D |
| `images/programcsupdatedwithDevPulseService.png` | IDE: `Program.cs` registration | C1b | 2D |
| `images/di-wiring-lines.jpg` | DI: Program → WeatherService → IndexModel | C2 | 2D |
| `images/index-twins-cshtml-vs-cs.jpg` | Two Index files — logic vs UI | C3 | 2E |
| `images/explorer-both-index-files.jpg` | Explorer: both Index files | C3b | 2E |
| `images/ongetasync-to-razor.jpg` | OnGetAsync fills · Razor shows | C4 | 2E–2F |
| `images/request-path-lines.jpg` | Browser → service → API → Razor | C5 | 2E |
| `images/data-property-path.jpg` | Follow `TemperatureC` via `.Data` | C6 | 2C / 2F |
| `images/csharp-case-sensitive.jpg` | `Ok`/`ok`, `WindKph`/`Windkph` | C6b | 2A |
| `images/replace-p-muted-weather-article.png` | Replace muted “coming soon” | C7 | 2F |
| `images/weathercard_completed.png` | Browser Weather card live | C8 | Checkpoint |

| Still optional | Topic |
|---|---|
| IDE JSON-map close-up | B14 crop |

**Do not use in PPT:** `images/genericvsconcreteclass.png` — prefer `generic_vs_concrete_class.png`.

---

## Slide index (build order)

### Act A — Models (destination)
| # | Slide | Image |
|---|---|---|
| A0 | How Models fit the data flow | `modeldataflowexplanation.png` |
| A1 | Create `Models/` | `createdmodelsfolder.png` |
| A2 | Create `CardResult.cs` | `createdcardresultscsfile.png` |
| A3 | Generics: one box | `generic_box_analogy.png` |
| A4 | Generics: car + engine bay | `genericautomotiveimage.png` |
| A5 | Type `CardResult.cs` | code (+ `csharp-case-sensitive.jpg` if needed) |
| A6 | Create `WeatherInfo.cs` | `createdweatherinfocsfile.png` |
| A7 | Generic vs concrete | `generic_vs_concrete_class.png` |
| A8 | Type `WeatherInfo.cs` | code |
| A9 | Models checkpoint | `modeldataflowexplanation.png` |

### Act B — WeatherService (journey)
| # | Slide | Image |
|---|---|---|
| B0 | Destination before journey | `modelsbeforeservice.jpg` |
| B1 | Starting point — Models exist | `a0f06680-263e-497e-b7c7-e08001a86514.jpg` |
| B2 | Create `Services/` + `WeatherService.cs` | `services-weatherservicecsimage.jpg` (+ `createservicesfolder.png`) |
| B3 | Why `System.Text.Json` | `when_to_use_system_text_json_v2.png` |
| B4 | `using` statements analogy | `systemtextjsonusingstatementsanalogy.png` |
| B5 | Trap — HttpClient in the Model | `puttinghttpclientcallsinthemodel.jpg` |
| B6 | Field first — slot + `readonly` | `field-slot-and-readonly.jpg` |
| B7 | Reading order — constructor comes after | `constructor-comes-after.jpg` |
| B8 | `readonly` is a rule, not a timeline | `readonly-rule-vs-constructor.jpg` |
| B9 | Constructor — HttpClient handed in | `constructor-DI.jpg` |
| B10 | How HttpClient gets into WeatherService | `http-to-weather-service.jpg` |
| B11 | Vocabulary — parameter vs field | `parameterandstorageweatherservice.jpg` |
| B12 | Common mix-ups | `_common_mixups_httpclient.jpg` |
| B13 | Order placed — `GetAsync` | `open-meteo-order-placed.jpg` |
| B14 | Map JSON → `WeatherInfo` | code (+ `data-property-path.jpg` preview) |
| B15 | One method · two doors | `try-catch-two-doors.jpg` |
| B16 | `CardResult<WeatherInfo>` is NOT inheritance | `cardresultisnotinheritance.jpg` |
| B17 | Inheritance vs generics | `genericsvsinheritance.jpg` |
| B18 | Checkpoint — ready for DI | `modelsandservicereadyforDI.jpg` |

### Act C — Wire & show
| # | Slide | Image |
|---|---|---|
| C0 | Weather file map | `weather-files-map.jpg` |
| C1 | What `AddHttpClient` does | `program-addhttpclient.jpg` |
| C1b | IDE `Program.cs` | `programcsupdatedwithDevPulseService.png` |
| C2 | DI wiring lines | `di-wiring-lines.jpg` |
| C3 | Two Index files | `index-twins-cshtml-vs-cs.jpg` |
| C3b | Explorer both Index files | `explorer-both-index-files.jpg` |
| C4 | OnGetAsync → Razor | `ongetasync-to-razor.jpg` |
| C5 | Request path | `request-path-lines.jpg` |
| C6 | Follow `.Data` | `data-property-path.jpg` |
| C7 | Replace muted paragraph | `replace-p-muted-weather-article.png` |
| C8 | `dotnet run` Weather card | `weathercard_completed.png` |

---

# ACT A — Models (destination)

Lab: **2A–2B** · Subject: typed shapes before any HTTP

---

<!-- (A0) -->

## A0 — How Models fit the data flow

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
- Banner: `Program.cs` registers `WeatherService` with DI (`AddHttpClient`) — comes in Act C.
- Dashed rule: **Models do NOT call the API; they only hold typed data.**
- Lab one-liner: **HTTP → JSON → C# models → Razor.**
- Mental model: API = waiter · `HttpClient` = order · JSON = plate · model = labels · Razor = food on the table.

**Checkpoint:** Student can point to Models and say “data shapes only.”

---

<!-- (A1) -->

## A1 — Create the `Models/` folder

**Image:** `images/createdmodelsfolder.png`  
**Goal:** Home for typed data before any API call.

**Speaker notes:**
- Create **`Models/`** at project root (sibling of `Pages/`, `wwwroot/`).
- Trap: not inside `wwwroot`.
- One-liner: “Models hold data. Services fetch data. Pages show data.”

**Checkpoint:** `Models/` next to `Pages/`.

---

<!-- (A2) -->

## A2 — Create `CardResult.cs`

**Image:** `images/createdcardresultscsfile.png`  
**Goal:** Shared success/error wrapper file exists.

**Speaker notes:**
- Inside `Models/`, create **`CardResult.cs`**.
- Will hold generic `CardResult<T>` — reused by every card later.
- File-exists beat first; generics pictures next; then type the body.

**Checkpoint:** `Models/CardResult.cs` visible.

---

<!-- (A3) -->

## A3 — Generics: one box design

**Image:** `images/generic_box_analogy.png`  
**Goal:** Make `<T>` obvious.

**Speaker notes:**
- Left: empty `CardResult<T>` — “Contents: ____”.
- Right: `WeatherInfo` / `GitHubStats` / `int` fillings.
- One design, reused — not three separate classes.
- `T` = sticky note: “fill me in later.”

**Checkpoint:** Why we don’t need `WeatherCardResult` and `HnCardResult`.

---

<!-- (A4) -->

## A4 — Generics: car + engine bay

**Image:** `images/genericautomotiveimage.png`  
**Goal:** Composition, not inheritance.

**Speaker notes:**
- Car body = `CardResult<T>` (Data / Error / Ok).
- Engine bay = `T`.
- **No inheritance** — car doesn’t become weather; it only checks if an engine is present.
- Full “Holding ≠ becoming” payoff lands at **B16–B17**.

**Checkpoint:** “Wrapper + payload,” not “WeatherInfo inherits CardResult.”

---

<!-- (A5) -->

## A5 — Type `CardResult.cs`

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

<!-- (A6) -->

## A6 — Create `WeatherInfo.cs`

**Image:** `images/createdweatherinfocsfile.png`  
**Goal:** Concrete weather payload file next to the wrapper.

**Speaker notes:**
- Create `WeatherInfo.cs` in `Models/`.
- Payload = temp / wind / summary. Envelope = `CardResult<T>`.
- Together later: `CardResult<WeatherInfo>`.

**Checkpoint:** Both model files exist.

---

<!-- (A7) -->

## A7 — Generic vs concrete

**Image:** `images/generic_vs_concrete_class.png`  
**Goal:** Contrast wrapper vs payload designs.

**Speaker notes:**
- Left: `CardResult<T>` — generic car body, engine bay blank on purpose.
- Right: `WeatherInfo` — concrete engine with fixed fields.
- Rule: generic when reusable across types; concrete when one known shape.
- `WeatherInfo` becomes `T` only inside `CardResult<WeatherInfo>`.

**Checkpoint:** Student can label wrapper vs payload.

---

<!-- (A8) -->

## A8 — Type `WeatherInfo.cs`

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
- Practice Qs live in `.facilitator/questions.md` (not linked from the public README).

**Checkpoint:** `CardResult<WeatherInfo>` = envelope + this payload.

---

<!-- (A9) -->

## A9 — Models checkpoint

**Image:** `images/modeldataflowexplanation.png` (Models panel)  

**Checklist:**
- [ ] `Models/` at project root  
- [ ] `CardResult.cs` + `WeatherInfo.cs` typed  
- [ ] Box + car + generic-vs-concrete analogies  
- [ ] Can narrate API → Service → Models → Page → Razor  

**Next:** Act B — why Models came before the Service, then build `WeatherService`.

---

# ACT B — WeatherService (journey)

Lab: **2C** · Subject: fetch + translate into the Model shapes  
**Teaching order:** destination → explorer → folder → usings → separation trap → field / `readonly` / file order → constructor hand-in → GetAsync → two doors → Holding ≠ becoming → ready for DI

---

<!-- (B0) -->

## B0 — Destination before journey

**Image:** `images/modelsbeforeservice.jpg`  
**Goal:** Why Models came *before* the Service.

**On slide (three panels):**
1. **MODELS (destination)** — `WeatherInfo` slots + `CardResult<T>` envelope · “Shape ready · no HTTP”
2. **SERVICE (journey)** — `HttpClient → JSON → fill WeatherInfo → wrap CardResult`
3. **PAGE (table)** — Weather card · same contract: Ok or Error

**Footer:** “Without Models, the Service has nowhere to put the JSON.”

**Speaker notes (<90 sec):**
- “Before we ever ask for data, we decide what the data looks like when it arrives — that’s a Model.”
- Service’s job = fetch + translate — needs a target shape.
- `CardResult<T>` = contract every future card will honor (success or error, never a crash).
- If students see `HttpClient` first, they fixate on network code and miss designing data shapes.

**Checkpoint:** Student can say why Models came first in one sentence.

---

<!-- (B1) -->

## B1 — Starting point — Models exist

**Image:** `images/a0f06680-263e-497e-b7c7-e08001a86514.jpg`  
**Goal:** Ground the room in the current Explorer state.

**Speaker notes:**
- `Models/` expanded: `CardResult.cs`, `WeatherInfo.cs`.
- Caption: **Services/ not created yet.**
- We’re about to add the journey layer next to the destination.

**Checkpoint:** No `Services/` folder yet — intentional.

---

<!-- (B2) -->

## B2 — Create `Services/` + `WeatherService.cs`

**Image:** `images/services-weatherservicecsimage.jpg`  
**Alt:** `images/createservicesfolder.png`  
**Goal:** Place for classes that *do* work (call APIs).

**Speaker notes:**
- Create **`Services/`** at project root (sibling of `Models/`) — same rule as Models: **not** inside `wwwroot`.
- Add `WeatherService.cs`.
- Services fetch + map JSON → models. Models still don’t fetch.

**Checkpoint:** `Services/WeatherService.cs` visible next to `Models/`.

---

<!-- (B3) -->

## B3 — Why `System.Text.Json`

**Image:** `images/when_to_use_system_text_json_v2.png`  
**Goal:** Justify the first `using` in `WeatherService`.

**Speaker notes:**
- Reading JSON? → need a library.
- No legacy Newtonsoft need? → **`System.Text.Json`** (built in, fast, no NuGet).
- Footer: WeatherService / Open-Meteo is the right default for the rest of DevPulse.

**Checkpoint:** Why we don’t `dotnet add package Newtonsoft.Json` for this lab.

---

<!-- (B4) -->

## B4 — `using` statements analogy

**Image:** `images/systemtextjsonusingstatementsanalogy.png`  
**Goal:** Explain the two usings at the top of `WeatherService.cs`.

**Speaker notes:**
- Pier = `WeatherService.cs`.
- Left line → `.NET` sea → reel in `System.Text.Json`.
- Right line → project pond → reel in `DevPulse.Models`.
- Caption: “casting a line to reel in tools and models from other namespaces.”

**Checkpoint:** Student can say what each `using` unlocks.

---

<!-- (B5) -->

## B5 — Trap — HttpClient in the Model

**Image:** `images/puttinghttpclientcallsinthemodel.jpg`  
**Goal:** Lock the separation before typing the field.

**Speaker notes:**
- Sticky: “Common mistake: putting HttpClient calls in the Model.”
- **Models describe. Services fetch.**
- If you’re calling `GetAsync` inside a Model file — stop, wrong class.
- Footer: **Shape ≠ Fetch.**

**Checkpoint:** Student can point to which folder is allowed to call the internet.

---

<!-- (B6) -->

## B6 — Field first — slot + `readonly`

**Image:** `images/field-slot-and-readonly.jpg`  
**Goal:** Declare the field before talking about the constructor.

**Code on slide:**

```csharp
private readonly HttpClient _httpClient;
```

**Callouts:**
- `HttpClient` → TYPE (from .NET)
- `_httpClient` → FIELD NAME (storage slot)
- `readonly` → RULE: may be assigned only once

**Speaker notes:**
- Title: **The field comes first.**
- This line does **NOT** run the constructor — it only declares a slot and a rule.
- Footer: Field = storage + rules. Nothing is stored yet.
- Matches live file order: field is above the constructor in `WeatherService.cs`.

**Checkpoint:** Student can say “slot + rule” without jumping to DI.

---

<!-- (B7) -->

## B7 — Reading order — constructor comes after

**Image:** `images/constructor-comes-after.jpg`  
**Goal:** Why the constructor sits *below* the field in the file.

**On slide:**
1. **DECLARE** — `private readonly HttpClient _httpClient;` · “slot + rule”
2. Arrow down — “later in the same file”
3. **ASSIGN (constructor)** — `_httpClient = httpClient;` · “obeys the rule — fills the slot once”

**Footer:** You do NOT need the constructor above the field. The field declares the rule. The constructor (below) obeys the rule.

**Speaker notes:**
- Reading order ≠ “constructor already happened.”
- Students often want to move the constructor up — don’t. Declare above, assign below.

**Checkpoint:** Student accepts field-above / constructor-below without confusion.

---

<!-- (B8) -->

## B8 — `readonly` is a rule, not a timeline

**Image:** `images/readonly-rule-vs-constructor.jpg`  
**Goal:** Kill the “readonly means the constructor already ran” myth.

**On slide:**
| WRONG | RIGHT |
|---|---|
| “readonly means the constructor already ran” | Field: “I have a slot. Assign it only once.” · Constructor (later): `_httpClient = httpClient;` |

**Takeaway:** When you see `readonly`, think: whatever fills this slot may do it only once — usually in a constructor below.

**Footer:** Rule first. Fulfillment second.

**Speaker notes:**
- Keep this slide correction-only — no DI diagram, no `GetAsync`.
- Ties B6 + B7 together before “HttpClient is handed in.”

**Checkpoint:** Student no longer equates `readonly` with “constructor already ran.”

---

<!-- (B9) -->

## B9 — Constructor — HttpClient is handed in

**Image:** `images/constructor-DI.jpg`  
**Goal:** The constructor is the one allowed fill; no `new HttpClient()`.

**Code callout:**

```csharp
public WeatherService(HttpClient httpClient)
{
    _httpClient = httpClient;
}
```

**Speaker notes:**
- Arrow on parameter: **We do NOT write `new HttpClient()`**.
- DI preview: “the app gives us `HttpClient`; we don’t create it.”
- Analogy on image: tray handed to the waiter before the shift.
- This is the fulfillment of the `readonly` rule from B6–B8.
- Full DI registration lands in Act C (`Program.cs`).

**Checkpoint:** Constructor receives `HttpClient`; it doesn’t construct it.

---

<!-- (B10) -->

## B10 — How HttpClient gets into WeatherService

**Image:** `images/http-to-weather-service.jpg`  
**Goal:** One clean left→right flow (no myth-busting on this slide).

**Four steps on slide:**
1. **App / DI (later)** — holds `HttpClient` · “hands in”
2. **Constructor** — `WeatherService(HttpClient httpClient)`
3. **Assignment** — `_httpClient = httpClient;` · “store once”
4. **WeatherService object** — field `private readonly HttpClient _httpClient;` · `GetAsync()` later

**Footer:** Outside hands it in → constructor receives it → field keeps it.

**Speaker notes:**
- Something outside (DI) creates `WeatherService`.
- Hands an `HttpClient` into the constructor.
- Constructor stores it in the field.
- Class keeps it for later methods.
- Do **not** pile myths onto this slide — that’s B11–B12.

**Checkpoint:** Student can narrate the four boxes in order.

---

<!-- (B11) -->

## B11 — Vocabulary — parameter vs field

**Image:** `images/parameterandstorageweatherservice.jpg`  
**Goal:** Same object, two names at two moments.

**On slide:**
| | Name | When |
|---|---|---|
| PARAMETER | `httpClient` | while being handed into the constructor |
| FIELD | `_httpClient` | while stored on the object |

**Footer:** The underscore is a naming habit for private fields — **not** security.

**Speaker notes:**
- Type is still `HttpClient` either way.
- Arrow: handed in → stored.
- Keep this slide vocabulary-only.

**Checkpoint:** Student can label parameter vs field on a blank signature.

---

<!-- (B12) -->

## B12 — Common mix-ups

**Image:** `images/_common_mixups_httpclient.jpg`  
**Goal:** Bust the three myths after vocabulary sticks.

| NOT this | Actually |
|---|---|
| “HttpClient is a readonly method” | `HttpClient` is a **TYPE**. `_httpClient` is a **FIELD**. |
| “public because it goes to the web” | `public` = other C# code can construct this. Web call is later in a method. |
| “`_httpClient` renamed for security” | `_` = private-field naming habit. Same object either way. |

**Footer:** `public ≠ internet · _ ≠ security · field ≠ method`

**Checkpoint:** Students can correct at least one myth without looking.

---

<!-- (B13) -->

## B13 — Order placed — talk to Open-Meteo

**Image:** `images/open-meteo-order-placed.jpg`  
**Goal:** The one line that talks to the internet.

**Code callout:**

```csharp
var url = "https://api.open-meteo.com/v1/forecast?...";
using var response = await _httpClient.GetAsync(url);
```

**Speaker notes:**
- Title beat: **Order placed.**
- Highlight: `GetAsync` / URL — “This one line talks to Open-Meteo.”
- Side label: **HttpClient = the order.**
- Uses the stored field `_httpClient` from B10–B11.

**Checkpoint:** Student knows which line leaves the process for the network.

---

<!-- (B14) -->

## B14 — Map JSON → `WeatherInfo`

**Image:** optional IDE crop of `WeatherService.GetAsync` (prompt 05 still available)  
**Goal:** Destination shape gets filled.

**Code (from live `WeatherService.cs`):**

```csharp
var current = doc.RootElement.GetProperty("current");
var temp = current.GetProperty("temperature_2m").GetDouble();
var wind = current.GetProperty("wind_speed_10m").GetDouble();

return new CardResult<WeatherInfo>
{
    Data = new WeatherInfo
    {
        TemperatureC = temp,
        WindKph = wind,
        Summary = $"{temp:0.#}°C · wind {wind:0.#} km/h",
    }
};
```

**Speaker notes:**
- API field names → our C# properties (`temperature_2m` → `TemperatureC`).
- This is why Models came first — the Service has somewhere to put the JSON.
- Payload goes in **`Data`**, not on `CardResult` itself (preview B16 / C5).

**Checkpoint:** Parsed values live on `WeatherInfo` inside `Data`.

---

<!-- (B15) -->

## B15 — One method · two doors

**Image:** `images/try-catch-two-doors.jpg`  
**Goal:** Never crash the whole page for one card.

**On slide:**
- **SUCCESS DOOR** — `return new CardResult<WeatherInfo> { Data = ... };`
- **ERROR DOOR** — `return new CardResult<WeatherInfo> { Error = "Couldn't load weather: ..." };`

**Footer:** `try/catch` turns network failure into a friendly card error.

**Speaker notes:**
- One method, two exits, same return type: `CardResult<WeatherInfo>`.
- Razor later branches on `Ok` / `Error` — same contract for every card.
- Ties back to Act A: the wrapper exists so this pattern is possible.

**Checkpoint:** Failure returns a card error, not an unhandled exception.

---

<!-- (B16) -->

## B16 — `CardResult<WeatherInfo>` is NOT inheritance

**Image:** `images/cardresultisnotinheritance.jpg`  
**Goal:** Holding ≠ becoming. Payload sits **in** `Data`.

**On slide:**
- Left: `CardResult<T>` = car body / bay · Data · Error · Ok
- Arrow: “T becomes WeatherInfo” · Generics = fill the blank · **NOT** inheritance
- Right: `CardResult<WeatherInfo>` = bay with THIS engine · TemperatureC / WindKph / Summary inside

**Rules:**
1. Holding ≠ becoming  
2. Read temp from `Data`: `result.Data.TemperatureC` — **NOT** `result.TemperatureC`

**Speaker notes:**
- Same car metaphor as A4 — now with the concrete fill.
- Mechanic checks the engine (`Data`), not the car-body paint (`CardResult`).

**Checkpoint:** Student can correct `result.TemperatureC` live.

---

<!-- (B17) -->

## B17 — Inheritance vs generics

**Image:** `images/genericsvsinheritance.jpg`  
**Goal:** Two C# patterns side by side before wiring the page.

**On slide:**
| Inheritance (`:`) — Is-A | Generics (`<>`) — Holds-A |
|---|---|
| `IndexModel : PageModel` | `CardResult<WeatherInfo>` |
| Get behaviors by **extending** | Wrapper holding a payload |
| Special kind of page model | Read payload from `Data` |

**Speaker notes:**
- Preview Act C: the page **inherits** `PageModel` (Is-A).
- The weather result **holds** `WeatherInfo` (Holds-A) — different pattern, same language.
- Don’t let students say “WeatherInfo extends CardResult.”

**Checkpoint:** Student can label Is-A vs Holds-A on a blank diagram.

---

<!-- (B18) -->

## B18 — Checkpoint — ready for DI

**Image:** `images/modelsandservicereadyforDI.jpg`  
**Goal:** Two folders · two jobs · next step is wiring.

**Checklist:**
- [ ] `Models/` — `CardResult.cs`, `WeatherInfo.cs`
- [ ] `Services/` — `WeatherService.cs` (field + constructor + GetAsync)
- [ ] Models describe · Services fetch
- [ ] Field declares rule · constructor fills once
- [ ] Holding ≠ becoming · read `.Data`
- [ ] Can narrate: DI will hand HttpClient → field → GetAsync → CardResult

**On slide caption:** Next — wire `WeatherService` in `Program.cs`.

**Next:** Act C.

---

# ACT C — Wire & show

Lab: **2D–2F** · Subject: DI → **two Index files** → Razor → run  
**Teaching order:** file map → `Program.cs` → DI lines → Index twins → OnGetAsync → request path → `.Data` → replace muted → run

---

<!-- (C0) -->

## C0 — Weather file map

**Image:** `images/weather-files-map.jpg`  
**Goal:** Orient before touching `Program.cs` / Index.

**Speaker notes:**
- Six boxes: WeatherInfo · CardResult · WeatherService · Program · Index.cshtml.cs · Index.cshtml
- Footer: Shape · Wrap · Fetch · Register · Store · Show
- Keep this slide up as a “you are here” map during Act C.

**Checkpoint:** Student can name which file owns `AddHttpClient` vs `@Model.Weather`.

---

<!-- (C1) -->

## C1 — What `AddHttpClient` does

**Image:** `images/program-addhttpclient.jpg`  
**Goal:** Concept before the IDE screenshot.

**Speaker notes:**
- `AddRazorPages` = pages work.
- `AddHttpClient<WeatherService>` = DI creates the service + hands it `HttpClient`.
- Without this line, `IndexModel(WeatherService weather)` has nothing to receive.

**Checkpoint:** Student can say why Program.cs matters before opening Index.

---

<!-- (C1b) -->

## C1b — IDE: `Program.cs` registration

**Image:** `images/programcsupdatedwithDevPulseService.png`  
**Goal:** Make the “App / DI hands in” box from B10 real.

**Code:**

```csharp
builder.Services.AddHttpClient<DevPulse.Services.WeatherService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
```

**Speaker notes:**
- Place **right after** `AddRazorPages();`
- Timeout = don’t hang forever.
- Matches README § 2D.

**Checkpoint:** App builds; `WeatherService` can be injected.

---

<!-- (C2) -->

## C2 — DI wiring lines (who creates what)

**Image:** `images/di-wiring-lines.jpg`  
**Goal:** Same pattern twice — constructor parameter → readonly field.

**On slide:**
1. **Program.cs** — `AddHttpClient<WeatherService>(...)`
2. **WeatherService.cs** — DI creates service + hands `HttpClient` → `_httpClient`
3. **Index.cshtml.cs** — DI hands `WeatherService` into `IndexModel` → `_weather`

**Footer:** Same pattern twice: constructor parameter → readonly field.

**Speaker notes:**
- Line 1: Program tells DI how to build `WeatherService`.
- Line 2: DI also hands that service into the page model.
- Students should recognize B6–B9 pattern repeating on `IndexModel`.

**Checkpoint:** Student can point to who creates `HttpClient` vs who creates `WeatherService`.

---

<!-- (C3) -->

## C3 — Index has TWO files

**Image:** `images/index-twins-cshtml-vs-cs.jpg`  
**Alt explorer:** `images/explorer-both-index-files.jpg`  
**Goal:** Kill the “I forgot there’s a .cs file” moment.

**On slide:**
| `Index.cshtml.cs` | `Index.cshtml` |
|---|---|
| Logic — fields, constructor, `OnGetAsync` | UI — HTML cards, `@if` |
| Sets `Weather = …` | Reads `Model.Weather` |

**Speaker notes:**
- Same page name · two jobs · two files.
- Sticky: don’t put `HttpClient` / `GetAsync` in the `.cshtml`.
- Open `.cs` to wire services; open `.cshtml` to show data.

**Checkpoint:** Student can say which file to open to call the service.

---

<!-- (C4) -->

## C4 — Page model — inject + `OnGetAsync`

**Image:** `images/ongetasync-to-razor.jpg`  
**Goal:** Page stores the result for Razor.

**Code:**

```csharp
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

**Speaker notes:**
- `: PageModel` = inheritance (Is-A) from B17.
- `_weather` field + constructor = same DI habit as `WeatherService`.
- Property `Weather` is what Razor reads as `Model.Weather`.
- Timeline: browser → OnGetAsync runs first → then Razor renders.

**Checkpoint:** Page compiles; `Weather` is filled on each GET.

---

<!-- (C5) -->

## C5 — Request path (line to line)

**Image:** `images/request-path-lines.jpg`  
**Goal:** Full path from browser to card text.

**Walk the arrows:**
- Browser GET `/` → page loads  
- `OnGetAsync` → `_weather.GetAsync()`  
- `_httpClient.GetAsync(url)` → Open-Meteo  
- JSON response back  
- `return CardResult` → `Weather =`  
- `Model.Weather` → Razor (`Ok` ? `Data.Summary` : `Error`)

**Footer:** Models describe · Service fetches · Page stores · Razor shows.

**Checkpoint:** Student can narrate the path without looking.

---

<!-- (C6) -->

## C6 — Follow `TemperatureC` / case sensitivity

**Images:** `images/data-property-path.jpg` · `images/csharp-case-sensitive.jpg`  
**Goal:** Kill `Model.Weather.TemperatureC` and casing bugs before Razor.

**Path:** `WeatherInfo.TemperatureC` → `CardResult.Data` → `IndexModel.Weather` → `Model.Weather.Data.Summary`

**WRONG:** `Model.Weather.TemperatureC`  
**ALSO WRONG:** `ok` instead of `Ok`, `Windkph` instead of `WindKph`

**Checkpoint:** Student writes `.Data.` and matches casing without prompting.

---

<!-- (C7) -->

## C7 — Replace muted paragraph in Weather article

**Image:** `images/replace-p-muted-weather-article.png`  
**Goal:** Exact place to edit in `Index.cshtml`.

**Speaker notes:**
- Must be inside `<article id="weather">` — not the HN card.
- Replace muted “coming soon” with the `@if` block.

**Target markup:**

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

**Checkpoint:** Markup is in the Weather article only.

---

<!-- (C8) -->

## C8 — `dotnet run` — Weather card live

**Image:** `images/weathercard_completed.png`  
**Goal:** End-to-end proof.

**Speaker notes:**
- Success = temperature summary line; failure = clear error (still a valid checkpoint).
- Hard-refresh if markup looks stale (Ctrl+Shift+R).

**Checkpoint:** HTTP → JSON → models → Razor works.

---

## Quick talk track (3 minutes)

1. **A0 / C0** — Big picture + file map.  
2. **A1–A8** — Models + generics.  
3. **B0 / B5** — Destination before journey · Shape ≠ Fetch.  
4. **B6–B12** — Field / `readonly` / hand-in / myths.  
5. **B13–B15** — Order placed → map JSON → two doors.  
6. **B16–B17** — Holding ≠ becoming · Is-A vs Holds-A.  
7. **C1–C5** — Program → DI → Index twins → request path.  
8. **C6–C8** — `.Data` + Razor + run.

---

## Facilitator tips

- Act A → B → C — never reverse. Destination before journey is the pedagogy.
- **Always teach C3 (two Index files)** before students edit markup — highest confusion point.
- Keep **B16 (not inheritance)** and **C6 (`.Data` path)** as a pair.
- Use **B17** right before `IndexModel : PageModel`.
- Gemini prompt recipes + practice questions stay in `.facilitator/` (gitignored). Polished images that README embeds stay in `images/` (committed).
- After any new screenshot, update **both** this inventory **and** [README.md](./README.md) in the same change.