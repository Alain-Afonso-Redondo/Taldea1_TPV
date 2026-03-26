using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Taldea1TPV.Eskariak
{
    internal class PlateraOsagaiakController
    {
        private readonly string _baseUrl = "http://localhost:5093/";

        
        // Plater baten osagaiak lortzea
        public List<PlateraOsagaiak> LortuOsagaiakPlateratik(int plateraId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync($"api/PlateraOsagaiak/platera/{plateraId}").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<PlateraOsagaiak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PlateraOsagaiak>>(json);
            }
        }

        
        // Osagaia plater batera gehitzea
        public bool GehituOsagaiaPlaterari(PlateraOsagaiak plateraOsagaia)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(plateraOsagaia);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/PlateraOsagaiak", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        
        // Plater baten osagaia ezabatzea
        public bool EzabatuOsagaiaPlateratik(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/PlateraOsagaiak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
