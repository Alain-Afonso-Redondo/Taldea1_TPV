using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Taldea1TPV.Eskariak
{
    internal class ErreserbakController
    {
        private readonly string _baseUrl = "http://localhost:5000/";

        
        // API-tik erreserba guztiak lortzea
        public List<Erreserba> LortuErreserbak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Erreserbak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Erreserba>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Erreserba>>(json);
            }
        }

        public List<Erreserba> LortuErreserbakData(DateTime data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                string dataStr = data.ToString("yyyy-MM-dd");
                var response = client.GetAsync($"api/Erreserbak/data/{dataStr}").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Erreserba>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Erreserba>>(json);
            }
        }

            

        // API bidez Erreserbak sortzea
        public Erreserba SortuErreserba(Erreserba erreserba)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(erreserba);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Erreserbak", content).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Erreserba>(responseJson);
            }
        }

        // Erreserba eguneratu Id-aren arabera
        public bool EguneratuErreserba(Erreserba erreserba)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(erreserba);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync($"api/Erreserbak/{erreserba.Id}", content).Result;
                return response.IsSuccessStatusCode;
            }
        }




        // Erreserba ezabatu Id-aren arabera
        public bool EzabatuErreserba(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Erreserbak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }

    }
}
