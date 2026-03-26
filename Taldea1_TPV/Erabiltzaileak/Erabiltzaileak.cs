using Newtonsoft.Json;

namespace Taldea1TPV
{
    public class Erabiltzaileak
    {
        [JsonProperty("id")]
        public virtual int Id { get; set; }

        [JsonProperty("erabiltzailea")]
        public virtual string Erabiltzailea { get; set; }

        [JsonProperty("emaila")]
        public virtual string Emaila { get; set; }

        [JsonProperty("rola")]
        public virtual string Rola { get; set; }

        [JsonProperty("txat")]
        public virtual bool Txat { get; set; }
    }
}
