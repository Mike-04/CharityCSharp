namespace Charity.Network.ObjectProtocol;

[Serializable]
public class GetDonatorsRequest : IRequest
{
    public string SearchString { get; set; }
    public GetDonatorsRequest(String searchString)
    {
        this.SearchString = searchString;
    }
}
