namespace Charity.Domain;

public class Donatie : Entity<Guid>
{
    public Donator Donator { get; set; }
    public CazCaritabil CazCaritabil { get; set; }
    public double SumaDonata { get; set; }
    public DateTime Timestamp { get; set; }

    public Donatie(Donator donator, CazCaritabil cazCaritabil, double sumaDonata) : base(Guid.NewGuid())
    {
        Donator = donator;
        CazCaritabil = cazCaritabil;
        SumaDonata = sumaDonata;
        Timestamp = DateTime.Now;
    }
    
    public Donatie(Guid id, Donator donator, CazCaritabil cazCaritabil, double sumaDonata,DateTime timestamp) : base(id)
    {
        Donator = donator;
        CazCaritabil = cazCaritabil;
        SumaDonata = sumaDonata;
        Timestamp = timestamp;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Donator: {Donator}, CazCaritabil: {CazCaritabil}, SumaDonata: {SumaDonata}, Timestamp: {Timestamp}";
    }
}