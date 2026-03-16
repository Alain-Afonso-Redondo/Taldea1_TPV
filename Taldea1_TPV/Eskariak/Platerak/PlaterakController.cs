using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Taldea1TPV.DTO;


namespace Taldea1TPV.Eskariak
{
    internal class PlaterakController
    {
        private readonly string _baseUrl = "http://192.168.2.101:5000/";

        
        // API-tik Plater guztiak lortzea
        public List<Platerak> LortuPlaterak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Platerak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Platerak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Platerak>>(json);
            }
        }

        
        // Platera lortzea Id-aren arabera
        public Platerak LortuPlatera(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Platerak/{id}").Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Platerak>(json);
            }
        }


        // Platerak lortzea kategoriaren arabera
        public List<PlaterakDto> LortuPlaterakKategoriatik(int kategoriaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.2.101:5000/");

                var response = client.GetAsync($"api/Platerak/kategoria/{kategoriaId}").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<PlaterakDto>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PlaterakDto>>(json);
            }
        }



        // Plater berria sortzea
        public bool SortuPlatera(Platerak platera)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(platera);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Platerak", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        
        // Platera ezabatzea Id-aren arabera
        public bool EzabatuPlatera(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Platerak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }
        // Karritora gehitzean plateraren stock-a jaistea
        public bool JaitsiStock(int platerId, int kopurua)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new
                {
                    PlaterId = platerId,
                    Kopurua = kopurua
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                
                var response = client.PostAsync("api/Platerak/jaitsi-stock", content).Result;

                return response.IsSuccessStatusCode;
            }
        }
        // Karritotik kentzean plateraren stock berreskuratzea
        public void ItzuliStock(int platerId, int kopurua)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new
                {
                    PlaterId = platerId,
                    Kopurua = kopurua
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

               
                client.PostAsync("api/Platerak/itzuli-stock", content).Wait();
            }
        }





    }
}
