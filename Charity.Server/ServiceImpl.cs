using Charity.Domain;
using Charity.Service;

namespace Charity.Server;

internal class ServiceImpl : IAppService
{
    private readonly IAppService innerService;
    
    public ServiceImpl(IAppService innerService)
    {
        this.innerService = innerService;
    }

    public User Login(string username, string password)
    {
        try
        {
            User user =innerService.Login(username, password);
            return user;
        }
        catch (Exception e)
        {
            throw new Exception("Login failed", e);
        }
    }

    public IEnumerable<CazCaritabil> GetAllCazuri()
    {
        try
        {
            IEnumerable<CazCaritabil> cazuri = innerService.GetAllCazuri();
            return cazuri;
        }
        catch (Exception e)
        {
            throw new Exception("GetAllCazuri failed", e);
        }
    }

    public IEnumerable<Donator> GetDonators(string searchString)
    {
        try
        {
            IEnumerable<Donator> donators = innerService.GetDonators(searchString);
            return donators;
        }
        catch (Exception e)
        {
            throw new Exception("GetDonators failed", e);
        }
    }
}