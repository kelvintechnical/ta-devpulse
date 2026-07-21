using System.Text.Json;
using DevPulse.Models;

namespace DevPulse.Services;

public class CryptoServices //the class which will hold the constructor and methods to get the Crypto data
{
    private readonly HttpClient _httpClient;

    public CryptoServices(HttpClient httpclient)
    {
        _httpClient = httpclient;
    }

    public async Task<CardResult<Crypto>> GetAsync() //the method which will get the Crypto data
    {
        try
        {
            // Work network blocks CoinGecko — using Frankfurter FX for local testing.
            // Course target (swap back off corporate firewall):
            // var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";
            // then: bitcoin.usd / ethereum.usd via GetProperty
            var url = "https://api.frankfurter.app/latest?from=USD&to=EUR,GBP";

            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var rates = doc.RootElement.GetProperty("rates");
            var eur = rates.GetProperty("EUR").GetDouble();
            var gbp = rates.GetProperty("GBP").GetDouble();
            var summary = $"USD→EUR {eur:0.####} · USD→GBP {gbp:0.####}";

            return new CardResult<Crypto>
            {
                Data = new Crypto
                {
                    Name = "USD FX (Frankfurter test)",
                    Symbol = "USD",
                    Price = eur,
                    Summary = summary,
                }
            };
        }
        catch (Exception ex)
        {
            return new CardResult<Crypto>
            {
                Error = $"Couldn't load Crypto: {ex.Message}"
            };
        }
    }
}
