using System;

namespace Taldea1TPV.Eskariak
{
    public class Fakturak
    {
        public virtual int Id { get; set; }
        public virtual int EskaeraId { get; set; }
        public virtual int? ErreserbaId { get; set; }
        public virtual double Totala { get; set; }
        public virtual string PdfIzena { get; set; }
        public virtual DateTime Data { get; set; }
    }
}
