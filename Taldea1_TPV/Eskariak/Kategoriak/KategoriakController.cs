using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Taldea1TPV.Eskariak
{
    internal class KategoriakController
    {
        private readonly string _baseUrl = "http://localhost:5000/";

        // API-tik Kategoria guztiak lortzea
        public List<Kategoriak> LortuKategoriak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Kategoriak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Kategoriak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Kategoriak>>(json);
            }
        }

     
        // Kategoria lortzea Id-aren arabera
        public Kategoriak LortuKategoria(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Kategoriak/{id}").Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Kategoriak>(json);
            }
        }

        
        // Kategoria berria sortzea
        public bool SortuKategoria(Kategoriak kategoria)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(kategoria);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Kategoriak", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        
        // Kategoria ezabatzea Id-aren arabera
        public bool EzabatuKategoria(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Kategoriak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
