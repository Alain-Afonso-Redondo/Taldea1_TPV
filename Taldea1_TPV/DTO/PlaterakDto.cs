using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taldea1TPV.DTO
{
    public class PlaterakDto
    {
        public int Id { get; set; }
        public string Izena { get; set; }
        public double Prezioa { get; set; }
        public int Stock { get; set; }

        public int KategoriaId { get; set; }
        public string KategoriaIzena { get; set; }
    }

}
