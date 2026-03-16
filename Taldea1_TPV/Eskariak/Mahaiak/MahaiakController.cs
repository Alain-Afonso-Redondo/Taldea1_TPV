using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Taldea1TPV.Eskariak
{
    internal class MahaiakController
    {
        private readonly string _baseUrl = "http://192.168.2.101:5000/";

        
        // API-tik Mahai guztiak lortzea
        public List<Mahaiak> LortuMahaiak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Mahaiak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Mahaiak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Mahaiak>>(json);
            }
        }

       
        // Mahaia lortzea Id-aren arabera
        public Mahaiak LortuMahaia(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Mahaiak/{id}").Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Mahaiak>(json);
            }
        }

       
        // Mahai berria sortzea
        public bool SortuMahaia(Mahaiak mahaia)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(mahaia);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Mahaiak", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        
        // Mahaia ezabatzea Id-aren arabera
        public bool EzabatuMahaia(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Mahaiak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
