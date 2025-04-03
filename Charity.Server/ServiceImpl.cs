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
}