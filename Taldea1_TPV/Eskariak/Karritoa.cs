using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taldea1TPV.Eskariak
{
    public class Karritoa
    {
        public int PlaterakId { get; set; }
        public string Izena { get; set; }
        public double Prezioa { get; set; }
        public int Kopurua { get; set; }

        public double Totala => Prezioa * Kopurua;
    }
}
