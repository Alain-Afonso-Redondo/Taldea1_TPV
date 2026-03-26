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

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = response.Content.ReadAsStringAsync().Result;
                var apiErantzuna = JsonConvert.DeserializeObject<ApiErantzuna<LoginErantzunaDto>>(json);
                var loginDatuak = apiErantzuna != null && apiErantzuna.Datuak != null
                    ? apiErantzuna.Datuak.FirstOrDefault()
                    : null;

                if (loginDatuak == null)
                    return null;

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
