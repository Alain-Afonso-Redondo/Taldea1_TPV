using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taldea1TPV.Inbentarioa;

namespace Taldea1TPV.Eskariak
{
    internal class PlateraOsagaiak
    {
        public virtual int Id { get; set; }
        public virtual Platerak Platera { get; set; }
        public virtual Osagaiak Osagaia { get; set; }
        public virtual int Kopurua { get; set; }
    }
}
