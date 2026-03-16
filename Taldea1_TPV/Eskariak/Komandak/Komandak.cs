namespace Taldea1TPV.Eskariak
{
    public class Komandak
    {
        public virtual int Id { get; set; }   
        public virtual int Kopurua { get; set; }
        public virtual double Totala { get; set; }
        public virtual string Oharrak { get; set; }
        public virtual bool Egoera { get; set; }

        public virtual Platerak Platerak { get; set; }
        public virtual int FakturakId { get; set; }

      
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

           Komandak other = (Komandak)obj;

            return Id == other.Id &&
                   Platerak != null &&
                   other.Platerak != null &&
                   Platerak.Id == other.Platerak.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (Platerak != null ? Platerak.Id.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
