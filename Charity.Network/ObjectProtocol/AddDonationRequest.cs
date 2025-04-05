using Charity.Network.DTO;

namespace Charity.Network.ObjectProtocol;
[Serializable]
public class AddDonationRequest : IRequest
{
    public CazCaritabilDTO CazCaritabil { get; set; }
    public DonatorDTO Donator { get; set; }
    public double SumaDonata { get; set; }
    
    public AddDonationRequest( DonatorDTO donator,CazCaritabilDTO cazCaritabil,double sumaDonata)
    {
        CazCaritabil = cazCaritabil;
        Donator = donator;
        SumaDonata = sumaDonata;
    }
    
}