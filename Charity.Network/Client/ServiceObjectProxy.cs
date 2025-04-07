using Charity.Domain;
using Charity.Network.DTO;
using Charity.Network.ObjectProtocol;
using Charity.Service;
using Charity.Service.Observer;

namespace Charity.Network.Client;

public class ServiceObjectProxy : ServiceObjectProxyBase,IAppService
{
    public ServiceObjectProxy(string host, int port) : base(host, port)
    {
        
    }
    
    public User Login(string username, string password,IObserver client)
    {
        InitializeConnection();
        Client = client;
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
        TestConnectionOpen();
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
        TestConnectionOpen();
        SendRequest(new GetDonatorsRequest(searchString));
        try
        {
            var resp = AwaitResponse<GetDonatorsResponse>();
            return resp.Donators.Select(d => d.ToDonator());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public void addDonation(Donator selectedDonor, CazCaritabil selectedCase, double v)
    {
        TestConnectionOpen();
        
        SendRequest(new AddDonationRequest(DonatorDTO.FromDonator(selectedDonor),CazCaritabilDTO.FromCazCaritabil(selectedCase),v));
        try
        {
            var resp = AwaitResponse<OkResponse>();
            if (resp == null)
                throw new ProxyException("Error adding donation");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public void AddCazCaritabil(string nume, double sumaAdunata)
    {
        TestConnectionOpen();
        SendRequest(new AddCazCaritabilRequest(nume, sumaAdunata));
        try
        {
            var resp = AwaitResponse<OkResponse>();
            if (resp == null)
                throw new ProxyException("Error adding charity case");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public void UpdateCazCaritabil(Guid id, string nume, double sumaAdunata)
    {
        TestConnectionOpen();
        SendRequest(new UpdateCazCaritabilRequest(id, nume, sumaAdunata));
        try
        {
            var resp = AwaitResponse<OkResponse>();
            if (resp == null)
                throw new ProxyException("Error updating charity case");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public void AddDonator(string nume, string adresa, string telefon)
    {
        TestConnectionOpen();
        SendRequest(new AddDonatorRequest(nume, adresa, telefon));
        try
        {
            var resp = AwaitResponse<OkResponse>();
            if (resp == null)
                throw new ProxyException("Error adding donor");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }

    public void UpdateDonator(Guid id, string nume, string adresa, string telefon)
    {
        TestConnectionOpen();
        SendRequest(new UpdateDonatorRequest(id, nume, adresa, telefon));
        try
        {
            var resp = AwaitResponse<OkResponse>();
            if (resp == null)
                throw new ProxyException("Error updating donor");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ProxyException(e);
        }
    }
    
    protected override void HandleUpdate(UpdateResponse update)
    {
        Console.WriteLine("HandleUpdate called");
        try
        {
            Client.Update();              
        }
        catch (Exception e)
        {
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