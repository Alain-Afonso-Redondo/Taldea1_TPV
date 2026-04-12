using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV.Eskariak
{
    internal class KomandakController
    {
        private readonly string _baseUrl = ApiConfig.BaseUrl;
        public string AzkenErrorea { get; private set; }

        public List<Komandak> LortuKomandak()
        {
            return new List<Komandak>();
        }

        public List<Komandak> LortuKomandakFakturatik(int fakturaId)
        {
            return new List<Komandak>();
        }

        public bool SortuKomanda(int fakturaId, int platerId, int kopurua)
        {
            string errorea;
            return SortuEskaera(
                fakturaId,
                0,
                1,
                new List<Karritoa>
                {
                    new Karritoa
                    {
                        PlaterakId = platerId,
                        Kopurua = kopurua,
                        Prezioa = 0
                    }
                },
                out errorea);
        }

        public bool SortuEskaera(int erabiltzaileId, int mahaiaId, int komensalak, List<Karritoa> karritoa, out string errorea)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new EskaeraSortuDto
                {
                    ErabiltzaileId = erabiltzaileId,
                    MahaiaId = mahaiaId,
                    Komensalak = komensalak,
                    Produktuak = karritoa.Select(k => new EskaeraProduktuaSortuDto
                    {
                        ProduktuaId = k.PlaterakId,
                        Kantitatea = k.Kopurua,
                        PrezioUnitarioa = Convert.ToDecimal(k.Prezioa)
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/eskaerak", content).Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    errorea = IrakurriErrorea(response);
                    AzkenErrorea = errorea;
                    return false;
                }

                errorea = null;
                return true;
            }
        }

        private static string IrakurriErrorea(HttpResponseMessage response)
        {
            try
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<string>>(json);

                if (erantzuna != null && !string.IsNullOrWhiteSpace(erantzuna.Message))
                    return erantzuna.Message;

                return string.IsNullOrWhiteSpace(json) ? response.ReasonPhrase : json;
            }
            catch
            {
                return response.ReasonPhrase;
            }
        }

        public bool EguneratuKomanda(Komandak komanda)
        {
            return false;
        }

        public bool EzabatuKomanda(int id)
        {
            return false;
        }
    }

    internal class EskaeraSortuDto
    {
        public int ErabiltzaileId { get; set; }
        public int MahaiaId { get; set; }
        public int Komensalak { get; set; }
        public List<EskaeraProduktuaSortuDto> Produktuak { get; set; }
    }

    internal class EskaeraProduktuaSortuDto
    {
        public int ProduktuaId { get; set; }
        public int Kantitatea { get; set; }
        public decimal PrezioUnitarioa { get; set; }
    }
}
