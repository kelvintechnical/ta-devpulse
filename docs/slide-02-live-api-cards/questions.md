# WeatherInfo.cs — 10 Practice Questions

**1. Why does `WeatherInfo.cs` not have a constructor?**
Because C#'s object initializer syntax (`new WeatherInfo { TemperatureC = 22 }`) already lets you set properties at creation — no need to write one manually.

**2. What would happen if you tried `weatherInfo.TemperatureC = "hot";`?**
Compile error — `TemperatureC` is `double`, not `string`. C# is statically typed, so mismatched types get caught before the app even runs.

**3. Why is `TemperatureC` a `double` instead of an `int`?**
Temperatures often need decimals (e.g. `22.5°C`), and `int` can't hold fractional values.

**4. If the weather API never returns wind data, what value would `WindKph` hold?**
`0.0` — its default. That's the same "0 vs missing" gotcha as `TemperatureC`: you can't tell "calm wind" from "never set" just by looking at the value.

**5. Why does `WeatherInfo` live in `Models/` and not `Services/`?**
`Models/` holds data shapes (describing *what* something is). `Services/` holds behavior (classes that *do* things, like call an API). `WeatherInfo` only stores data — no API logic — so it belongs in `Models/`.

**6. Could `WeatherInfo` implement `Ok`, like `CardResult<T>` does?**
Technically yes, but it wouldn't make sense here — `Ok` checks for success/failure, and `WeatherInfo` isn't a success/failure wrapper. That job belongs to `CardResult<WeatherInfo>`, which wraps a `WeatherInfo` instance.

**7. What's the difference between `WeatherInfo` and `CardResult<WeatherInfo>`?**
`WeatherInfo` is the raw data (temperature, wind, summary). `CardResult<WeatherInfo>` is the wrapper *around* that data — it adds the success/failure layer (`Ok`, `Error`) on top.

**8. Why default `Summary` to `""` instead of something like `"N/A"`?**
Either works technically — `""` is just the conventional "safe empty" choice in C#. The point isn't the specific text, it's guaranteeing the value is never `null` so Razor never crashes reading it.

**9. If you added a fourth property, `public string City { get; set; } = "";`, would anything else in `WeatherInfo.cs` need to change?**
No — properties are independent of each other. You could add, remove, or rename one without touching `TemperatureC`, `WindKph`, or `Summary`.

**10. Who is responsible for actually filling in `TemperatureC`, `WindKph`, and `Summary` with real values?**
`WeatherService` — `WeatherInfo` itself never populates its own data; it's just the shape waiting to be filled by whatever class calls the weather API.