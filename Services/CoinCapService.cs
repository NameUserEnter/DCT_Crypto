using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DCT_Crypto.Models;

namespace CryptoApp.Services
{
    public class CoinCapService
    {
        private readonly HttpClient _httpClient;

        public CoinCapService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/");
        }

        public async Task<List<Asset>> GetCurrencyModels()
        {
            List<Asset> cryptos = new List<Asset>();

            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.coincap.io/v2/assets";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonresponse = await response.Content.ReadAsStringAsync();
                    dynamic r = JsonConvert.DeserializeObject(jsonresponse);
                    foreach (var a in r.data)
                    {
                        Asset crypto = new Asset()
                        {
                            Id = a.id,
                            Rank = a.rank,
                            Symbol = a.symbol,
                            Name = a.name,
                            Supply = a.supply,
                            MaxSupply = a.maxSupply,
                            MarketCapUsd = a.marketCapUsd,
                            VolumeUsd24Hr = a.volumeUsd24Hr,
                            PriceUsd = a.priceUsd,
                            ChangePercent24Hr = a.changePercent24Hr,
                            Vwap24Hr = a.vwap24Hr,
                            Explorer = a.explorer
                        };
                        cryptos.Add(crypto);
                    }
                }
                else
                {
                    Console.WriteLine($"Error fetching cryptocurrencies: {response.StatusCode}");
                }
            }

            return cryptos;
        }
    }
}
