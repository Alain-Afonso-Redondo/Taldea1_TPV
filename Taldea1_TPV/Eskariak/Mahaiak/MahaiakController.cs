using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class MahaiakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;

        public List<Mahaiak> LortuMahaiak(DateTime? data = null, string txanda = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var url = "api/mahaiak";

                if (data.HasValue)
                {
                    var txandaQuery = Uri.EscapeDataString(txanda ?? string.Empty);
                    url += string.Format(
                        "?data={0:yyyy-MM-dd}&txanda={1}",
                        data.Value.Date,
                        txandaQuery);
                }

                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Mahaiak>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<MahaiaApiDto>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.Select(MapToMahaiak).ToList()
                    : new List<Mahaiak>();
            }
        }

        public Mahaiak LortuMahaia(int id, DateTime? data = null, string txanda = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var url = string.Format("api/mahaiak/{0}", id);

                if (data.HasValue)
                {
                    var txandaQuery = Uri.EscapeDataString(txanda ?? string.Empty);
                    url += string.Format(
                        "?data={0:yyyy-MM-dd}&txanda={1}",
                        data.Value.Date,
                        txandaQuery);
                }

                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<MahaiaApiDto>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.Select(MapToMahaiak).FirstOrDefault()
                    : null;
            }
        }

        private static Mahaiak MapToMahaiak(MahaiaApiDto mahaia)
        {
            return new Mahaiak
            {
                Id = mahaia.Id,
                Zenbakia = mahaia.Zenbakia,
                Kapazitatea = mahaia.Kapazitatea,
                Egoera = mahaia.Egoera
            };
        }

        private class MahaiaApiDto
        {
            public int Id { get; set; }

            public int Zenbakia { get; set; }

            public int Kapazitatea { get; set; }

            public string Egoera { get; set; }
        }
    }
}
