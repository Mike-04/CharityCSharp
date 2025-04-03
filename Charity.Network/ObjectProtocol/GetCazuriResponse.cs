using Charity.Domain;
using Charity.Network.DTO;

namespace Charity.Network.ObjectProtocol;
[Serializable]
public class GetCazuriResponse : IResponse
{
    public IEnumerable<CazCaritabilDTO> Cazuri { get; set; }

    public GetCazuriResponse(IEnumerable<CazCaritabilDTO> cazuri)
    {
        Cazuri = cazuri;
    }
}