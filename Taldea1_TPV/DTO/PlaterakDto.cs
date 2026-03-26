using System.Collections.Generic;

namespace Taldea1TPV.DTO
{
    public class PlaterakDto
    {
        public int Id { get; set; }

        public string Izena { get; set; }

        public double Prezioa { get; set; }

        public int stock_aktuala { get; set; }

        public int kategoria_id { get; set; }

        public string KategoriaIzena { get; set; }
    }

    internal class ApiErantzuna<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public List<T> Datuak { get; set; }
    }
}
