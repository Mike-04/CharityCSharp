using Charity.Domain;
using Charity.Service.Observer;

namespace Charity.Service;

public interface IAppService
{
    public User Login(string username, string password,IObserver client);
    public IEnumerable<CazCaritabil> GetAllCazuri();
    public IEnumerable<Donator> GetDonators(String searchString);
    public void addDonation(Donator selectedDonor, CazCaritabil selectedCase, double v);
    public void AddCazCaritabil(string nume, double sumaAdunata);
    public void UpdateCazCaritabil(Guid id,string nume, double sumaAdunata);
    public void AddDonator(string nume, string adresa, string telefon);
    public void UpdateDonator(Guid id, string nume, string adresa, string telefon);
}