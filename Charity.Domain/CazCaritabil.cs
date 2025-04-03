namespace Charity.Domain;

public class CazCaritabil : Entity<Guid>
    {
        public string Nume { get; set; }
        public double SumaAdunata { get; set; }

        public CazCaritabil(string nume, double sumaAdunata) : base(Guid.NewGuid())
        {
            
            Nume = nume;
            SumaAdunata = sumaAdunata;
        }
        
        public CazCaritabil(Guid id, string nume, double sumaAdunata) : base(id)
        {
            Nume = nume;
            SumaAdunata = sumaAdunata;
        }

        public void AdaugaDonatie(double suma)
        {
            SumaAdunata += suma;
        }
        
        public override string ToString()
        {
            return $"Id: {Id}, Nume: {Nume}, SumaAdunata: {SumaAdunata}";
        }
    }
