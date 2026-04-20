using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class KategoriakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;

        public List<Kategoriak> LortuKategoriak()
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync("api/kategoriak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Kategoriak>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Kategoriak>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                    : new List<Kategoriak>();
            }
        }

        public Kategoriak LortuKategoria(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync(string.Format("api/kategoriak/{0}", id)).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<Kategoriak>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;
            }
        }

        public bool SortuKategoria(Kategoriak kategoria)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var json = JsonConvert.SerializeObject(kategoria);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/kategoriak", content).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public bool EzabatuKategoria(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.DeleteAsync(string.Format("api/kategoriak/{0}", id)).Result;
                return response.IsSuccessStatusCode;
            }
        }
    }
}

