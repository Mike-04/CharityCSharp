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

    public void addDonation(Donator selectedDonor, CazCaritabil selectedCase, double v)
    {
        try
        {
            innerService.addDonation(selectedDonor, selectedCase, v);
        }
        catch (Exception e)
        {
            throw new Exception("AddDonation failed", e);
        }
    }

    public void AddCazCaritabil(string nume, double sumaAdunata)
    {
        try
        {
            innerService.AddCazCaritabil(nume, sumaAdunata);
        }
        catch (Exception e)
        {
            throw new Exception("AddCazCaritabil failed", e);
        }
    }

    public void UpdateCazCaritabil(Guid id, string nume, double sumaAdunata)
    {
        try
        {
            innerService.UpdateCazCaritabil(id, nume, sumaAdunata);
        }
        catch (Exception e)
        {
            throw new Exception("UpdateCazCaritabil failed", e);
        }
    }

    public void AddDonator(string nume, string adresa, string telefon)
    {
        try
        {
            innerService.AddDonator(nume, adresa, telefon);
        }
        catch (Exception e)
        {
            throw new Exception("AddDonator failed", e);
        }
    }

    public void UpdateDonator(Guid id, string nume, string adresa, string telefon)
    {
        try
        {
            innerService.UpdateDonator(id, nume, adresa, telefon);
        }
        catch (Exception e)
        {
            throw new Exception("UpdateDonator failed", e);
        }
    }
}