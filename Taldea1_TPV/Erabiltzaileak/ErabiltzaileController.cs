using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Taldea1TPV.DTO;

namespace Taldea1TPV
{
    internal static class ApiConfig
    {
        public static string BaseUrl
        {
            get
            {
                var value = ConfigurationManager.AppSettings["ApiBaseUrl"];
                return string.IsNullOrWhiteSpace(value) ? "http://localhost:5093/" : value;
            }
        }
    }

    internal class ErabiltzaileController
    {
        private const int ZerbitzariaRolaId = 2;
        public string AzkenErrorea { get; private set; }

        public Erabiltzaileak BalidatuLogin(string erabiltzailea, string pasahitza)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(ApiConfig.BaseUrl);

                var body = new
                {
                    erabiltzailea = erabiltzailea,
                    pasahitza = pasahitza,
                    txat = false
                };

                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/login", content).Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var apiErantzuna = JsonConvert.DeserializeObject<ApiErantzuna<LoginErantzunaDto>>(json);
                var loginDatuak = apiErantzuna != null && apiErantzuna.Datuak != null
                    ? apiErantzuna.Datuak.FirstOrDefault()
                    : null;

                if (loginDatuak == null)
                {
                    AzkenErrorea = "Login erantzuna baliogabea da.";
                    return null;
                }

                if (loginDatuak.Rola == null || loginDatuak.Rola.Id != ZerbitzariaRolaId)
                {
                    AzkenErrorea = "Ezin zara sartu: rola 2 (zerbitzaria) behar da.";
                    return null;
                }

                return new Erabiltzaileak
                {
                    Id = loginDatuak.Id,
                    Erabiltzailea = loginDatuak.Erabiltzailea,
                    Emaila = loginDatuak.Emaila,
                    Rola = loginDatuak.Rola != null ? loginDatuak.Rola.Izena : null,
                    Txat = loginDatuak.Txat
                };
            }
        }

        public bool? LortuTxatBaimena(int erabiltzaileId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(ApiConfig.BaseUrl);

                var response = client.GetAsync("api/login/" + erabiltzaileId + "/txat").Result;
                AzkenErrorea = null;

                if (!response.IsSuccessStatusCode)
                {
                    AzkenErrorea = IrakurriErrorea(response);
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var apiErantzuna = JsonConvert.DeserializeObject<ApiErantzuna<bool>>(json);
                var badagoDaturik = apiErantzuna != null &&
                                    apiErantzuna.Datuak != null &&
                                    apiErantzuna.Datuak.Any();

                if (!badagoDaturik)
                {
                    AzkenErrorea = "Txat baimenaren erantzuna baliogabea da.";
                    return null;
                }

                return apiErantzuna.Datuak.First();
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
    }

    internal class LoginErantzunaDto
    {
        public int Id { get; set; }

        public string Erabiltzailea { get; set; }

        public string Emaila { get; set; }

        public LoginRolaDto Rola { get; set; }

        public bool Txat { get; set; }
    }

    internal class LoginRolaDto
    {
        public int Id { get; set; }

        public string Izena { get; set; }
    }
}
