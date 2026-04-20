using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class FakturakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;
        public string AzkenErrorea { get; private set; }

        public List<Fakturak> LortuFakturak()
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync("api/Fakturak").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return new List<Fakturak>();
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Fakturak>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                    : new List<Fakturak>();
            }
        }

        public Fakturak LortuFaktura(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Fakturak/{id}").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Fakturak>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;
            }
        }

        public Fakturak LortuFakturaErreserbarenArabera(int erreserbaId)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Fakturak/erreserba/{erreserbaId}").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Fakturak>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;
            }
        }

        public Fakturak SortuEdoLortuFakturaErreserbatik(int erreserbaId)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var dto = new { ErreserbaId = erreserbaId };
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Fakturak/sortu-erreserbatik", content).Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var resultJson = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Fakturak>>(resultJson);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;
            }
        }

        public bool EzabatuFaktura(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Fakturak/{id}").Result;
                AzkenErrorea = response.IsSuccessStatusCode ? null : IrakurriErrorea(response);
                return response.IsSuccessStatusCode;
            }
        }

        public bool EguneratuTotala(int fakturaId, double gehikuntza)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new
                {
                    FakturaId = fakturaId,
                    Gehikuntza = gehikuntza
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Fakturak/eguneratu-totala", content).Result;
                AzkenErrorea = response.IsSuccessStatusCode ? null : IrakurriErrorea(response);
                return response.IsSuccessStatusCode;
            }
        }

        private static string IrakurriErrorea(HttpResponseMessage response)
        {
            try
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<string>>(json);

                if (erantzuna != null && !string.IsNullOrWhiteSpace(erantzuna.Message))
                    return erantzuna.Message;

                return string.IsNullOrWhiteSpace(json) ? response.ReasonPhrase : json;
            }
            catch
            {
                return response.ReasonPhrase;
            }
        }
    }
}

