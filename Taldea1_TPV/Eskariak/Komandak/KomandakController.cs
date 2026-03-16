using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Taldea1TPV.Eskariak
{
    internal class KomandakController
    {
        private readonly string _baseUrl = "http://192.168.2.101:5000/";

        
        // API-tik Komanda guztiak lortzea
        public List<Komandak> LortuKomandak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var response = client.GetAsync("api/Komandak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Komandak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Komandak>>(json);
            }
        }

        
        // Faktura baten Komandak lortzea
        public List<Komandak> LortuKomandakFakturatik(int fakturaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

              
                var response = client.GetAsync($"api/Komandak/faktura/{fakturaId}").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Komandak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Komandak>>(json);
            }
        }


        // Komanda berria sortzea
        public bool SortuKomanda(int fakturaId, int platerId, int kopurua)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.2.101:5000/");

                var body = new
                {
                    FakturakId = fakturaId,
                    PlaterakId = platerId,
                    Kopurua = kopurua
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Komandak", content).Result;
                return response.IsSuccessStatusCode;
            }
        }







        // Komanda eguneratzea
        public bool EguneratuKomanda(Komandak komanda)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var json = JsonConvert.SerializeObject(komanda);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                
                var response = client.PutAsync($"api/Komandak/{komanda.Id}", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        
        // Komanda ezabatzea Id-aren arabera
        public bool EzabatuKomanda(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Komandak/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
