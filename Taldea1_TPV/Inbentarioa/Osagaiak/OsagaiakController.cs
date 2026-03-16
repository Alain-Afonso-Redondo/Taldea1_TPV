using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Taldea1TPV.Inbentarioa
{
    internal class OsagaiakController
    {
        private readonly string _baseUrl = "http://192.168.2.101:5000/";

        // API-tik osagai guztiak lortzea
        public List<Osagaiak> LortuOsagaiak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Osagaiak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Osagaiak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Osagaiak>>(json);
            }
        }
    }
}
