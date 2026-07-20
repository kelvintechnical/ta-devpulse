# DevPulse — Deck 2: Build (ASP.NET / C#)

**When:** Most of the morning (projector stays here)  
**Goal:** Follow-along build, one step at a time  
**Sync rule:** Slide numbers match README steps  
**Source of truth for typing:** root [`README.md`](../../README.md) — attendees type the code; slides stay short (goal / focus / checkpoint). Do not pre-build Services or Models for the room.

**Slide template (every step):** Title · Goal · Focus · Screenshot · Checkpoint

---

## Slide 0 — Setup
**On slide:** .NET 8 SDK · `dotnet new webapp` · `dotnet run` · [SCREENSHOT: app in browser]

**Goal:** Everyone can run a Razor Pages app locally.

**Focus:** Open the `DevPulse` project (or create it), run `dotnet run`, open the localhost URL.

**Speaker notes:**
- Plain English: the SDK compiles C# and starts a local web server.
- Confirm: terminal shows a listening URL (e.g. `http://localhost:5xxx`).
- **Checkpoint:** Default or starter page loads in the browser.

---

## Slide 1 — The Skeleton
**On slide:** Five empty cards in Razor + CSS grid · [SCREENSHOT: 5 empty cards]

**Goal:** Layout before data — five labeled card shells.

**Focus:** `Index.cshtml` card markup + `wwwroot/css/site.css` grid.

**Speaker notes:**
- Why skeleton first: if layout is broken, API data won’t save you.
- Cards: Weather, Hacker News, GitHub, Crypto, Challenge.
- **Checkpoint:** Five empty cards with titles visible.

---

## Slide 2 — Weather (first HttpClient call)
**On slide:** Open-Meteo · service + model · [SCREENSHOT: temp showing]

**Goal:** First real API call in C#; show temperature on the page.

**Focus:** `WeatherService` using `HttpClient` → deserialize JSON → bind on Index page.

**Speaker notes:**
- Reuse waiter metaphor: order → plate → model → Razor.
- Register service in `Program.cs` (DI).
- `try/catch` + error message on the card.
- **Checkpoint:** Weather card shows a temperature (or clear weather line).

---

## Slide 3 — Hacker News
**On slide:** Top IDs → `Task.WhenAll` → top 5 headlines · [SCREENSHOT: headlines]

**Goal:** Multiple requests in parallel; list results.

**Focus:** Fetch top story IDs, take 5, `Task.WhenAll` for details, render titles/links.

**Speaker notes:**
- `Task.WhenAll` = start several downloads together, wait for all.
- Loading/error states per card.
- **Checkpoint:** Five HN titles visible.

---

## Slide 4 — GitHub Trending
**On slide:** Search API + date math · sort by stars · [SCREENSHOT: repos]

**Goal:** Filter recent repos; show name + stars.

**Focus:** Build “created after” date; call GitHub Search; order by stars.

**Speaker notes:**
- Date math in plain English: “repos from the last week.”
- User-Agent header required by GitHub — set it on HttpClient.
- Friendly error if rate-limited.
- **Checkpoint:** Short list of repos + star counts.

---

## Slide 5 — Crypto Ticker
**On slide:** CoinGecko · auto-refresh · [SCREENSHOT: BTC/ETH]

**Goal:** Prices that update without a manual rebuild.

**Focus:** Fetch prices in a service; page meta-refresh every 60s (simple, teachable).

**Speaker notes:**
- Meta refresh = browser reloads the page on a timer (good enough for the lab).
- Don’t hammer free APIs — 60 seconds is fine.
- **Checkpoint:** BTC/ETH show; after ~60s the page refreshes.

---

## Slide 6 — Daily Challenge
**On slide:** Local list + form button + random · [SCREENSHOT: challenge]

**Goal:** Interaction without an API — array + random pick via form post.

**Focus:** `ChallengeService` list; button posts to a page handler; show picked challenge.

**Speaker notes:**
- Contrast: not everything needs the network.
- Razor Pages handler = C# method that runs when the form submits.
- **Checkpoint:** Clicking the button shows a new challenge.

---

## Slide 7 — Stuck? (common errors)
**On slide:**
- `dotnet` not found → install .NET 8 SDK, restart terminal  
- Port in use → stop old `dotnet run` or use another port  
- Null / empty card → check model property names vs JSON  
- GitHub 403 → User-Agent missing or rate limit  
- Red errors in terminal → read the stack trace; fix one issue at a time  

**Speaker notes:**
- Normalize debugging: terminal output is a friend.
- Park deep questions for break / Ship deck.

---

## Slide 8 — Build complete → Ship next
**On slide:** All five cards working locally · next: Azure (Deck 3)

**Speaker notes:**
- Celebration beat.
- Switch to **Deck 3 — Ship & Close**.

---

## Facilitation tips (not a slide)
- Stop after every step for test + screenshot.
- Explain *before* code; narrate *after*.
- Faster attendees: improve error copy or CSS — don’t invent new features mid-lab.
