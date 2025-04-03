using Charity.Network.DTO;

namespace Charity.Network.ObjectProtocol;

[Serializable]
public class GetDonatorsResponse : IResponse
{
    public IEnumerable<DonatorDTO> Donators { get; set; }
    
        public GetDonatorsResponse(IEnumerable<DonatorDTO> donators)
        {
            Donators = donators;
        }
    
}