using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class PlaterakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;

        public List<Platerak> LortuPlaterak()
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync("api/produktuak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Platerak>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<PlaterakDto>>(json);

                if (erantzuna == null || erantzuna.Datuak == null)
                    return new List<Platerak>();

                return erantzuna.Datuak.Select(MapToPlatera).ToList();
            }
        }

        public Platerak LortuPlatera(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync(string.Format("api/produktuak/{0}", id)).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<PlaterakDto>>(json);
                var platera = erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;

                return platera == null ? null : MapToPlatera(platera);
            }
        }

        public List<PlaterakDto> LortuPlaterakKategoriatik(int kategoriaId)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync("api/produktuak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<PlaterakDto>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<PlaterakDto>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.Where(p => p.kategoria_id == kategoriaId).ToList()
                    : new List<PlaterakDto>();
            }
        }

        public bool SortuPlatera(Platerak platera)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var json = JsonConvert.SerializeObject(platera);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/produktuak", content).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public bool EzabatuPlatera(int id)
        {
            using (var client = ApiClientFactory.Create())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.DeleteAsync(string.Format("api/produktuak/{0}", id)).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public bool JaitsiStock(int platerId, int kopurua)
        {
            var platera = LortuPlatera(platerId);
            return platera != null && platera.Stock >= kopurua;
        }

        public void ItzuliStock(int platerId, int kopurua)
        {
        }

        private static Platerak MapToPlatera(PlaterakDto platera)
        {
            return new Platerak
            {
                Id = platera.Id,
                Izena = platera.Izena,
                Prezioa = platera.Prezioa,
                Stock = platera.stock_aktuala,
                Kategoriak = new Kategoriak
                {
                    Id = platera.kategoria_id
                }
            };
        }
    }
}

