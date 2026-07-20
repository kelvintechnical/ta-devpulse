# DevPulse — Deck 1: Kickoff (ASP.NET / C#)

**When:** Before anyone runs `dotnet`  
**Goal:** Align the room on what we’re building, pre-lab setup, and how the morning works  
**File to open live:** this deck only, then switch to `02-build`

---

## Slide 1 — Title
**On slide:** DevPulse — Build in Public · Tech Affiliates · Giddy-Up · date · QR to repo

**Speaker notes:**
- Welcome. One sentence: today we build a live C# dashboard and put it on Azure.
- QR → GitHub README → catch up at the current step.
- Stack: ASP.NET Core Razor Pages, .NET 8, no database, no API keys.

---

## Slide 2 — What is DevPulse?
**On slide:** One web app. Five live cards. Real APIs. Azure URL by the end.

**Cards:** Weather · Hacker News · GitHub Trending · Crypto · Daily Challenge

**Speaker notes:**
- Server-rendered pages in C# — the browser gets finished HTML.
- End state: open your Azure link on your phone and see live data.

---

## Slide 3 — End state
**On slide:** Screenshot placeholder of finished dashboard · “This is where we’re going”

**Speaker notes:**
- Show the finished board so beginners aren’t guessing.
- Promise: follow checkpoints → leave with a live URL.

---

## Slide 4 — Stack rules
**On slide:**
- ASP.NET Core Razor Pages (.NET 8)
- C# services + models + Razor HTML/CSS
- No database · no API keys
- Run locally with `dotnet run`
- Deploy: Azure App Service

**Speaker notes:**
- Pre-install matters — this is not a no-install HTML lab.
- If someone asks about JavaScript: the browser still shows HTML; our logic is C# on the server.

---

## Slide 5 — Concepts map (waiter metaphor)
**On slide:**
- API = the waiter / kitchen
- `HttpClient` = placing your order
- JSON = the plate that comes back
- C# model = labeling what’s on the plate
- Razor = putting food on *your* table
- `async` / `await` = waiting politely for the kitchen
- `try` / `catch` = kitchen closed → friendly message

**Speaker notes:**
- Teach once up front; repeat during Build when people get stuck.

---

## Slide 6 — Pre-lab checklist (do before the session)
**On slide:**
- [ ] .NET 8 SDK installed (`dotnet --version`)
- [ ] Cursor or VS Code + C# support
- [ ] GitHub account
- [ ] Free Azure account (or use demo deploy at the end)
- [ ] Repo cloned / QR ready

**Speaker notes:**
- Night-before email should include the SDK install link.
- Backup: your portfolio Azure URL if someone’s machine fails.

---

## Slide 7 — Agenda & timeboxes
**On slide:**
- Kickoff — 10–15 min
- Build (steps 0–6) — ~90–120 min
- Break — as needed
- Ship & close — ~20 min

**Speaker notes:**
- Mixed-skill room: faster people help neighbors.
- Parking lot for deep questions.

---

## Slide 8 — How we work today
**On slide:** Build → `dotnet run` → browser check → Screenshot → Next step

**Speaker notes:**
- Every step has a checkpoint.
- Switch projector to **Deck 2 — Build** after this slide.

---

## After this deck
Open `decks/02-build/slides.md` and start Step 0.  
Attendees follow **README.md** for what to type line-by-line.
