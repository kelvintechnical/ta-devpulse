# Slide 3 — Ship & Close

**Status:** Upcoming  
**Slides:** _Google Slides link coming soon_  
**Repo:** [github.com/kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse) · **Site:** [kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)

> **Goal:** Polish the portfolio bar, deploy to Azure App Service, and leave with a live URL + resume talking points.

**Prev:** [Slide 2 — Live API Cards](../slide-02-live-api-cards/README.md) · [← README](../../README.md)

---

## Polish

### Step 6.5 — Polish (portfolio bar)

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

Do these before you call the project “done”:

1. **Every card** has success UI **or** a clear `.error` message (never blank).  
2. **Timeouts** on all `HttpClient` registrations (10s is fine).  
3. **README live URL** placeholder filled after Azure deploy.  
4. Optional: last-updated time: `@DateTime.Now.ToString("t")` in the hero.  
5. Optional: trim Privacy nav link if you do not need it.

---

## Ship to Azure

### Step 7 — Ship to Azure

---

> **Visual check:** After the checkpoint, compare your browser to the card on your running app. We’ll add a committed screenshot here once this step is demoed live.

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

Put that URL at the **top of this README** under **Live demo**.

---

## What you can say you learned

- What an API is  
- How `HttpClient` requests data in C#  
- Mapping JSON to models  
- Why `async` / `await` matters  
- Razor Pages (server-rendered HTML)  
- Light dependency injection  
- Guarding failures with `try` / `catch`  
- Deploying ASP.NET to Azure  

**Resume line:** *Built and deployed a multi-API Razor Pages dashboard in C# (.NET 8).*

---

## Slide 3 checkpoint

- [ ] Live HTTPS URL opens on a phone
- [ ] Demo URL filled into the root README
