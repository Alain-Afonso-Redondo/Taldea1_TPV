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

        public EskaeraAktiboa LortuEskaeraAktiboaMahaika(int mahaiaId, DateTime? data = null, string txanda = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var url = $"api/eskaerak/mahaia/{mahaiaId}/aktiboa";

                if (data.HasValue)
                {
                    var txandaQuery = Uri.EscapeDataString(txanda ?? string.Empty);
                    url += $"?data={data.Value:yyyy-MM-dd}&txanda={txandaQuery}";
                }

                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<EskaeraAktiboaDto>>(json);
                var dto = erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;

                if (dto == null)
                    return null;

                return new EskaeraAktiboa
                {
                    Id = dto.Id,
                    MahaiaId = dto.MahaiaId,
                    Komensalak = dto.Komensalak,
                    Egoera = dto.Egoera,
                    SukaldeaEgoera = dto.SukaldeaEgoera
                };
            }
        }

        public List<Karritoa> LortuEskaeraProduktuak(int eskaeraId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var response = client.GetAsync($"api/eskaerak/{eskaeraId}/produktuak").Result;

                if (!response.IsSuccessStatusCode)
                    return new List<Karritoa>();

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<EskaeraProduktuaLortuDto>>(json);

                return erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak
                        .GroupBy(p => new { p.ProduktuaId, p.ProduktuaIzena, p.PrezioUnitarioa })
                        .Select(g => new Karritoa
                        {
                            PlaterakId = g.Key.ProduktuaId,
                            Izena = g.Key.ProduktuaIzena,
                            Prezioa = Convert.ToDouble(g.Key.PrezioUnitarioa),
                            Kopurua = g.Sum(x => x.Kantitatea)
                        })
                        .ToList()
                    : new List<Karritoa>();
            }
        }

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
                null,
                DateTime.Today,
                "Bazkaria",
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

        public bool SortuEskaera(
            int erabiltzaileId,
            int mahaiaId,
            int komensalak,
            int? erreserbaId,
            DateTime? data,
            string txanda,
            List<Karritoa> karritoa,
            out string errorea)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new EskaeraSortuDto
                {
                    ErabiltzaileId = erabiltzaileId,
                    MahaiaId = mahaiaId,
                    Komensalak = komensalak,
                    ErreserbaId = erreserbaId,
                    Data = data?.Date,
                    Txanda = txanda,
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

        public bool EguneratuEskaera(int eskaeraId, int komensalak, List<Karritoa> karritoa, out string errorea)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var body = new EskaeraEguneratuDto
                {
                    Komensalak = komensalak,
                    Produktuak = karritoa.Select(k => new EskaeraProduktuaEditatuDto
                    {
                        ProduktuaId = k.PlaterakId,
                        Kantitatea = k.Kopurua
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PutAsync($"api/eskaerak/{eskaeraId}", content).Result;
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

        public bool OrdaintzeraBidali(int eskaeraId, out string errorea)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.PostAsync($"api/eskaerak/{eskaeraId}/ordainduEskaera", null).Result;
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

        public string SortuFaktura(int eskaeraId, out string errorea)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.PostAsync($"api/eskaerak/{eskaeraId}/sortuFaktura", null).Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    errorea = IrakurriErrorea(response);
                    AzkenErrorea = errorea;
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var erantzuna = JsonConvert.DeserializeObject<ApiErantzuna<string>>(json);
                var path = erantzuna != null && erantzuna.Datuak != null
                    ? erantzuna.Datuak.FirstOrDefault()
                    : null;

                errorea = null;
                return path;
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
        public int? ErreserbaId { get; set; }
        public DateTime? Data { get; set; }
        public string Txanda { get; set; }
        public List<EskaeraProduktuaSortuDto> Produktuak { get; set; }
    }

    internal class EskaeraProduktuaSortuDto
    {
        public int ProduktuaId { get; set; }
        public int Kantitatea { get; set; }
        public decimal PrezioUnitarioa { get; set; }
    }

    internal class EskaeraEguneratuDto
    {
        public int Komensalak { get; set; }
        public List<EskaeraProduktuaEditatuDto> Produktuak { get; set; }
    }

    internal class EskaeraProduktuaEditatuDto
    {
        public int ProduktuaId { get; set; }
        public int Kantitatea { get; set; }
    }

    internal class EskaeraAktiboa
    {
        public int Id { get; set; }
        public int MahaiaId { get; set; }
        public int Komensalak { get; set; }
        public string Egoera { get; set; }
        public string SukaldeaEgoera { get; set; }
    }

    internal class EskaeraAktiboaDto
    {
        public int Id { get; set; }
        public int MahaiaId { get; set; }
        public int Komensalak { get; set; }
        public string Egoera { get; set; }
        public string SukaldeaEgoera { get; set; }
    }

    internal class EskaeraProduktuaLortuDto
    {
        public int ProduktuaId { get; set; }
        public string ProduktuaIzena { get; set; }
        public decimal PrezioUnitarioa { get; set; }
        public int Kantitatea { get; set; }
    }
}
