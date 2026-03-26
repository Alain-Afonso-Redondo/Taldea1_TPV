using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Windows.Forms;

namespace Taldea1TPV.Eskariak
{
    internal class FakturakController
    {
        private readonly string _baseUrl = "http://localhost:5093/";

        // ================== GET GUZTIAK ==================
        public List<Fakturak> LortuFakturak()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync("api/Fakturak").Result;
                if (!response.IsSuccessStatusCode)
                    return new List<Fakturak>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Fakturak>>(json);
            }
                
        }

        // ================== GET ID ==================
        public Fakturak LortuFaktura(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Fakturak/{id}").Result;
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Fakturak>(json);
            }
                
        }

        // ================== GET ERRESERBA ==================
        public Fakturak LortuFakturaErreserbarenArabera(int erreserbaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Fakturak/erreserba/{erreserbaId}").Result;
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Fakturak>(json);
            }
                
        }


        public Fakturak SortuEdoLortuFakturaErreserbatik(int erreserbaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var dto = new { ErreserbaId = erreserbaId };
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/Fakturak/sortu-erreserbatik", content).Result;
                if (!response.IsSuccessStatusCode)
                    return null;

                var resultJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Fakturak>(resultJson);
            }
           
        }


        // ================== EZABATU ==================
        public bool EzabatuFaktura(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync($"api/Fakturak/{id}").Result;
                return response.IsSuccessStatusCode;
            }
                
        }

        // ================== TOTALA EGUNERATU ==================
        public bool EguneratuTotala(int fakturaId, double gehikuntza)
        {
            using (var client = new HttpClient())
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
                return response.IsSuccessStatusCode;
            }
                
        }

        //public string SortuTiketa(int fakturaId, double jasotakoa, string ordainketaModua)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(_baseUrl);

        //        var dto = new
        //        {
        //            FakturaId = fakturaId,
        //            Jasotakoa = jasotakoa,
        //            OrdainketaModua = ordainketaModua
        //        };

        //        var json = JsonConvert.SerializeObject(dto);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = client.PostAsync("api/Fakturak/sortu-tiketa", content).Result;
        //        if (!response.IsSuccessStatusCode)
        //            return null;

        //        return response.Content.ReadAsStringAsync().Result
        //            .Replace("\"", "");
        //    }
            
        //}




    }
}
