# DevPulse — Deck 3: Ship & Close (Azure)

**When:** After all five cards work locally  
**Goal:** Deploy, celebrate, name what you learned, send people home with a path forward  
**File to open live:** after Build slide 8

---

## Slide 1 — Ship it
**On slide:** Azure App Service · “Your app → a real URL” · [SCREENSHOT: Azure portal or publish]

**Goal:** Everyone understands the deploy path; demo URL goes live.

**Focus:** Publish the Razor app to Azure App Service (portal, VS/Cursor publish, or Azure CLI).

**Speaker notes:**
- Deploy = host the compiled app so anyone can open it.
- If Azure signup is slow: project your portfolio deploy; attendees finish signup at home with README steps.
- **Checkpoint:** Live HTTPS URL opens on a phone.

---

## Slide 2 — You shipped
**On slide:** Big live URL (or QR) · “This is on the internet”

**Speaker notes:**
- Celebration — don’t rush.
- Optional: open a few attendee URLs if they published successfully.

---

## Slide 3 — What you can say you learned
**On slide:**
- What an API is  
- How `HttpClient` requests data in C#  
- Mapping JSON to models  
- Why `async` / `await` matters  
- Razor Pages (server-rendered HTML)  
- Light dependency injection  
- Guarding failures with `try` / `catch`  
- Deploying ASP.NET to Azure  

**Speaker notes:**
- Resume line: “Built and deployed a multi-API Razor Pages dashboard in C#.”
- Repeat waiter metaphor once more.

---

## Slide 4 — Your repo & how to keep going
**On slide:** GitHub QR · README = step guide · keep the Azure URL

**Speaker notes:**
- Repo is the lasting handout.
- Encourage cloning and re-running `dotnet run` at home.

---

## Slide 5 — Stretch ideas (optional)
**On slide:**
- Clearer per-card loading states  
- Last-updated timestamps  
- Stronger mobile CSS  
- Swap meta-refresh for a timed partial update later  

**Speaker notes:**
- Not homework — inspiration for portfolio polish.

---

## Slide 6 — Group photo + CTA
**On slide:** “You built this” · group photo · follow @kelvinintech · thank Giddy-Up

**Speaker notes:**
- Photo for #buildinpublic.
- Thank hosts and helpers.

---

## Slide 7 — Close
**On slide:** “APIs aren’t magic. You just ordered from the kitchen — in C#.”

**Speaker notes:**
- End on confidence.
- Point deep rabbit holes to next session / office hours.

---

## After the session (facilitator checklist — not a slide)
- [ ] Push final public repo (learner-completed code + README)
- [ ] Post live Azure URL + screenshots
- [ ] Drop screenshots into decks where `[SCREENSHOT]` remains
- [ ] Note what confused the room → tweak README / Build slides for next run
- [ ] Convert stabilized `slides.md` files into three `.pptx` decks
- [ ] Confirm README still teaches typing — no “finished app” spoiling the lab
