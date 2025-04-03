using System.Globalization;
using Charity.Domain;

namespace Charity.Network.DTO;

[Serializable]
public class DonatieDTO : EntityDTO<Guid>
{
    public DonatorDTO Donator { get; set; }
    public CazCaritabilDTO CazCaritabil { get; set; }
    public double SumaDonata { get; set; }
    public DateTime Timestamp { get; set; }
    public DonatieDTO(Guid id,Donator donator, CazCaritabil cazCaritabil, double sumaDonata, DateTime timestamp) : base(id)
    {
        Donator = DonatorDTO.FromDonator(donator);
        CazCaritabil = CazCaritabilDTO.FromCazCaritabil(cazCaritabil);
        SumaDonata = sumaDonata;
        Timestamp = timestamp;
    }
    
    public static DonatieDTO FromDonatie(Donatie donatie) 
    {
        return new DonatieDTO(donatie.Id, donatie.Donator, donatie.CazCaritabil, donatie.SumaDonata, donatie.Timestamp);
    }
    
    public Donatie ToDonatie()
    {
        return new Donatie(Id, Donator.ToDonator(), CazCaritabil.ToCazCaritabil(), SumaDonata, Timestamp);
    }
}