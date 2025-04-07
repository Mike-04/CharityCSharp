using Charity.Domain;
using Charity.Service;
using Charity.Service.Observer;

namespace Charity.Server;

internal class ServiceImpl : IAppService ,ISubject
{
    private readonly IAppService innerService;
    private readonly List<IObserver> observers = new List<IObserver>();
    
    public ServiceImpl(IAppService innerService)
    {
        this.innerService = innerService;
    }

    public User Login(string username, string password,IObserver client)
    {
        try
        {
            User user =innerService.Login(username, password,client);
            RegisterObserver(client);
            Console.WriteLine($"User {username} logged in");
            //write the observer to console
            Console.WriteLine($"Observer {client.ToString()} registered");
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
            NotifyObservers();
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
            NotifyObservers();
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
            NotifyObservers();
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
            NotifyObservers();
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
            NotifyObservers();
        }
        catch (Exception e)
        {
            throw new Exception("UpdateDonator failed", e);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }
}