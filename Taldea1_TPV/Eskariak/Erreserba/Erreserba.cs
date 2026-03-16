using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;


namespace Taldea1TPV.Eskariak
{
    public class Erreserba
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual string Telefonoa { get; set; }
        public virtual string Txanda { get; set; }
        public virtual int PertsonaKopurua { get; set; }
        public virtual DateTime Data { get; set; }

        public virtual List<Mahaiak> Mahaiak { get; set; } = new List<Mahaiak>();


    }
}
