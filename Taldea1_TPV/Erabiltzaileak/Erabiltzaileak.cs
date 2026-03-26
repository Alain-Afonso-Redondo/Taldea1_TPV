namespace Taldea1TPV
{
    public class Erabiltzaileak
    {
        public virtual int Id { get; set; }

        public virtual string Erabiltzailea { get; set; }

        public virtual string Emaila { get; set; }

        public virtual string Rola { get; set; }

        public virtual bool Txat { get; set; }
    }
}
