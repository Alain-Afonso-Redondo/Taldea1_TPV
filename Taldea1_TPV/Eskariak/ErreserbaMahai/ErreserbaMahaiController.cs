using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Taldea1TPV.Eskariak
{
    public class ErreserbaMahaiController
    {
        private readonly string _baseUrl = "http://localhost:5000/";

        public bool GehituMahaiErreserbara(int erreserbaId, int mahaiId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var dto = new ErreserbaMahai
                {
                    ErreserbakId = erreserbaId,
                    MahaiakId = mahaiId
                };

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/ErreserbaMahaiak", content).Result;
                return response.IsSuccessStatusCode;
            }
                
        }

        public List<int> LortuMahaiakErreserbarentzat(int erreserbaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client
                    .GetAsync($"api/ErreserbaMahaiak/erreserba/{erreserbaId}")
                    .Result;

                if (!response.IsSuccessStatusCode)
                    return new List<int>();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<int>>(json);
            }
        }

        public bool EguneratuMahaiErreserban(int erreserbaId, int mahaiIdBerria)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                
                var deleteResp = client.DeleteAsync(
                    $"api/ErreserbaMahaiak/erreserba/{erreserbaId}"
                ).Result;

                if (!deleteResp.IsSuccessStatusCode)
                    return false;

               
                var dto = new ErreserbaMahai
                {
                    ErreserbakId = erreserbaId,
                    MahaiakId = mahaiIdBerria
                };

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var postResp = client.PostAsync("api/ErreserbaMahaiak", content).Result;
                return postResp.IsSuccessStatusCode;
            }
        }

        public bool EzabatuMahaiakErreserbatik(int erreserbaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.DeleteAsync(
                    $"api/ErreserbaMahaiak/erreserba/{erreserbaId}"
                ).Result;

                return response.IsSuccessStatusCode;
            }
        }



    }
}
