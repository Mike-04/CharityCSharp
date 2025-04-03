using Charity.Domain;
using Charity.Network.ObjectProtocol;
using Charity.Service;

namespace Charity.Network.Client;

public class ServiceObjectProxy : ServiceObjectProxyBase,IAppService
{
    public ServiceObjectProxy(string host, int port) : base(host, port)
    {
        
    }
    
    public User Login(string username, string password)
    {
        InitializeConnection();
        SendRequest(new LoginUserRequest(username, password));
        try
        {
            var resp = AwaitResponse<LoginUserResponse>();
            return resp.User.ToUser();
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public IEnumerable<CazCaritabil> GetAllCazuri()
    {
        InitializeConnection();
        SendRequest(new GetCazuriRequest());
        try
        {
            var resp = AwaitResponse<GetCazuriResponse>();
            return resp.Cazuri.Select(c => c.ToCazCaritabil());
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public IEnumerable<Donator> GetDonators(string searchString)
    {
        InitializeConnection();
        SendRequest(new GetDonatorsRequest(searchString));
        try
        {
            var resp = AwaitResponse<GetDonatorsResponse>();
            return resp.Donators.Select(d => d.ToDonator());
        }
        catch (Exception e)
        {
            CloseConnection();
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    private R AwaitResponse<R>() where R : class, IResponse
    {
        var resp = ReadResponse();
        if (resp is ErrorResponse)
            throw new ErrorResponseException((resp as ErrorResponse).Message);
        if (!(resp is R))
            throw new ProxyException($"Wrong response: expected {typeof(R).Name}, received {resp.GetType().Name}");
        return resp as R;
    }
}