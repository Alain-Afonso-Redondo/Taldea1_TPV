namespace Taldea1TPV.Eskariak
{
    public class Platerak
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual double Prezioa { get; set; }
        public virtual int Stock { get; set; }

        public virtual Kategoriak Kategoriak { get; set; }
    }
}
