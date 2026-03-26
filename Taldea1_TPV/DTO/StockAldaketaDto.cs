using System.Collections.Generic;

namespace Taldea1TPV.DTO
{
    public class StockAldaketaDto
    {
        public int PlaterId { get; set; }
        public int Kopurua { get; set; }
    }

    internal class ApiErantzuna<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public List<T> Datuak { get; set; }
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
