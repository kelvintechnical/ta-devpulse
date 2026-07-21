# Session notes — where we left off

**Date:** 2026-07-21  
**Focus:** Slide 3 HTTP cards (copy Weather path)

## Working

| Card | Status |
|---|---|
| **Weather** | Done — Open-Meteo, `WeatherService`, Index + Razor |
| **GitHub** | Done — single repo `kelvintechnical/ta-devpulse`, User-Agent required |
| **Crypto** | Wired end-to-end, but **not on CoinGecko yet** (see below) |

## Paused here — Crypto API

- Work network **blocks CoinGecko** (`403` / filtered).
- Temporarily using **Frankfurter** FX for testing the same JSON → model → card path:  
  `https://api.frankfurter.app/latest?from=USD&to=EUR,GBP`
- **Next (off work network):** switch URL back to course CoinGecko simple price:  
  `https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd`  
  Map `bitcoin.usd` / `ethereum.usd`. Comments are already in `Services/CryptoServices.cs`.

## Not started / placeholder only

| Card | Status |
|---|---|
| **Hacker News** | Model exists (`Models/HackerNews.cs`); no service / Index / Razor wiring yet |
| **Daily Challenge** | Placeholder card only (`ChallengeServices.cs` stub) |

## Reminders that burned us (keep handy)

1. **One `IndexModel`** — inject every service into the same page model (not a second `*Model` class).
2. **Field + ctor** — `private readonly X _x;` then `_x = x;` or `OnGetAsync` can’t see it.
3. **JSON shape must match URL** — array → `EnumerateArray().First()`; object → `GetProperty(...)`. Don’t use `items` unless the payload has `items`.
4. **GitHub needs User-Agent** on `AddHttpClient`.
5. **Razor shows `Summary`** — if API `description` is null, the card looks “empty.”

## Resume checklist

1. Confirm Frankfurter crypto card shows rates at work (or CoinGecko at home).
2. Build **Hacker News** (same Weather recipe — Slide 3 README).
3. Build **Daily Challenge** (local POST, not HTTP GET).
4. Optional: meta-refresh for crypto; Azure ship.
