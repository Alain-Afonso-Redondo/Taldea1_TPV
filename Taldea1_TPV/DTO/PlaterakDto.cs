using Newtonsoft.Json;

namespace Taldea1TPV.DTO
{
    public class PlaterakDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("izena")]
        public string Izena { get; set; }

        [JsonProperty("prezioa")]
        public double Prezioa { get; set; }

        [JsonProperty("stock_aktuala")]
        public int Stock { get; set; }

        [JsonProperty("kategoria_id")]
        public int KategoriaId { get; set; }

        public string KategoriaIzena { get; set; }
    }
}
