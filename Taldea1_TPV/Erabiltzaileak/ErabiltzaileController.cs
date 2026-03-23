using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Taldea1TPV
{
    internal class ErabiltzaileController
    {
        public bool BalidatuLogin(string erabiltzailea, string pasahitza)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("http://localhost:5000/");

                
                var response = client.GetAsync("api/Erabiltzailea").Result;

                if (!response.IsSuccessStatusCode)
                    return false;

                var json = response.Content.ReadAsStringAsync().Result;

                
                var apiErabiltzaileak = JsonConvert.DeserializeObject<List<Erabiltzaileak>>(json);

               
                return apiErabiltzaileak.Any(e =>
                    e.Izena == erabiltzailea &&
                    e.Pasahitza == pasahitza
                );
            }
        }

    }
}
