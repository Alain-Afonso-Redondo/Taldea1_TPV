using System.Collections.Generic;
using Newtonsoft.Json;

namespace Taldea1TPV.DTO
{
    public class StockAldaketaDto
    {
        public int PlaterId { get; set; }
        public int Kopurua { get; set; }
    }

    internal class ApiErantzuna<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("datuak")]
        public List<T> Datuak { get; set; }
    }

    internal class LoginErantzunaDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("erabiltzailea")]
        public string Erabiltzailea { get; set; }

        [JsonProperty("emaila")]
        public string Emaila { get; set; }

        [JsonProperty("rola")]
        public LoginRolaDto Rola { get; set; }

        [JsonProperty("txat")]
        public bool Txat { get; set; }
    }

    internal class LoginRolaDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("izena")]
        public string Izena { get; set; }
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
