using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class ErreserbakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;
        public string AzkenErrorea { get; private set; }

        public List<Erreserba> LortuErreserbak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync("api/Erreserbak").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return new List<Erreserba>();
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Erreserba>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                    : new List<Erreserba>();
            }
        }

        public List<Erreserba> LortuErreserbakData(DateTime data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var dataStr = data.ToString("yyyy-MM-dd");
                var response = client.GetAsync($"api/Erreserbak/data/{dataStr}").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return new List<Erreserba>();
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Erreserba>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                    : new List<Erreserba>();
            }
        }

        public Erreserba SortuErreserba(Erreserba erreserba)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(erreserba);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/Erreserbak", content).Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var responseJson = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Erreserba>>(responseJson);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;
            }
        }

        public bool EguneratuErreserba(Erreserba erreserba)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(erreserba);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PutAsync($"api/Erreserbak/{erreserba.Id}", content).Result;
                AzkenErrorea = response.IsSuccessStatusCode ? null : IrakurriErrorea(response);

                return response.IsSuccessStatusCode;
            }
        }

        public bool EzabatuErreserba(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.DeleteAsync($"api/Erreserbak/{id}").Result;
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
