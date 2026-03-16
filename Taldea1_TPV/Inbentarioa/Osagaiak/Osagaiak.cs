using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taldea1TPV.Inbentarioa
{
    public class Osagaiak
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual double azkenPrezioa { get; set; }
        public virtual int Stock { get; set; }
        public virtual int gutxienekoStock { get; set; }
        public virtual bool eskatu { get; set; }
    }
}
