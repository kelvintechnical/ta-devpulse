# DevPulse

**Live multi-API developer dashboard in ASP.NET Core** · Built in public · Aimed at mid-level .NET

**Repo:** [https://github.com/kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse)  
**Site:** [www.kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)  
**Live demo:** `_your-azure-url-here_`

> *Target one-liner:* Built a production-minded ASP.NET Core dashboard that aggregates live developer signals via typed HttpClient services, caching, per-source failure isolation, EF Core prefs, auth, automated tests, and Azure CI/CD.

**Stack (target):** ASP.NET Core Razor Pages · .NET 8 · C# · EF Core · Identity/OAuth · Azure App Service · GitHub Actions

---

## Lab guides (3 slides)

Work through these **three pages** in order:

| Slide | Page | Status |
|---|---|---|
| **1** | [Dashboard Skeleton (Razor + CSS)](docs/slide-01-dashboard-skeleton/README.md) · [Google Slides](https://docs.google.com/presentation/d/1_EAkSfcNAW6q6c8elsMYhZ75GI4uQd-r4UIkOaBu7tw/edit?usp=sharing) | **Done** |
| **2** | [Live API Cards](docs/slide-02-live-api-cards/README.md) | Next |
| **3** | [Ship & Close](docs/slide-03-ship-and-close/README.md) | Upcoming |

---

## What it does

Five cards on one dashboard: **Weather** · **Hacker News** · **GitHub Trending** · **Crypto** · **Daily Challenge**

---

## Run locally

```bash
cd DevPulse
dotnet run
```

Needs the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

---

## Current status

| Layer | Status |
|---|---|
| Slide 1 — skeleton + dark CSS | **Done** |
| Slide 2 — live API cards | **Next** |
| Slide 3 — Azure ship | Upcoming |
| Mid-level extras (cache, EF, auth, tests, CI) | Planned |

Overall mid-tier product: ~**35–40%**

---

## Repo layout

```text
ta-devpulse/
  README.md
  docs/
    slide-01-dashboard-skeleton/   ← Slide 1 page + images
    slide-02-live-api-cards/       ← Slide 2 page
    slide-03-ship-and-close/       ← Slide 3 page
  DevPulse/                        ← ASP.NET Core app
```

---

## License / credit

Tech Affiliates · Build in public · Greenville NC  
[kelvintechnical/ta-devpulse](https://github.com/kelvintechnical/ta-devpulse) · [kelvinintechconsulting.com](https://www.kelvinintechconsulting.com)
