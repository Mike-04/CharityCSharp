using Charity.Domain;

namespace Charity.Service;

public interface IAppService
{
    public User Login(string username, string password);
    public IEnumerable<CazCaritabil> GetAllCazuri();
}