using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    public class ErreserbaMahaiController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;

        public bool GehituMahaiErreserbara(int erreserbaId, int mahaiId)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var dto = new ErreserbaMahai
                {
                    ErreserbakId = erreserbaId,
                    MahaiakId = mahaiId
                };

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/ErreserbaMahaiak", content).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public List<int> LortuMahaiakErreserbarentzat(int erreserbaId)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync($"api/ErreserbaMahaiak/erreserba/{erreserbaId}").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<int>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<int>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                    : new List<int>();
            }
        }

        public bool EguneratuMahaiErreserban(int erreserbaId, int mahaiIdBerria)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var deleteResp = client.DeleteAsync($"api/ErreserbaMahaiak/erreserba/{erreserbaId}").Result;
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
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.DeleteAsync($"api/ErreserbaMahaiak/erreserba/{erreserbaId}").Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}

