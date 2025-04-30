using Charity.Domain;
using Charity.Service;
using Charity.Service.Observer;

namespace Charity.Server;

internal class ServiceImpl : IAppService ,ISubject
{
    private readonly IAppService innerService;
    //map of users and their observers
    private Dictionary<string, IObserver> observers = new Dictionary<string, IObserver>();
    
    
    public ServiceImpl(IAppService innerService)
    {
        this.innerService = innerService;
    }

    public User Login(string username, string password,IObserver client)
    {
        try
        {
            User user =innerService.Login(username, password,client);
            observers.Add(user.Username, client);
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

    public void Logout(String username, IObserver client)
    {
        try
        {
            innerService.Logout(username, client);
            Console.WriteLine($"User {username} logged out");
            //remove the observer from the map
            if (observers.ContainsKey(username))
            {
                observers.Remove(username);
                Console.WriteLine($"Observer {client.ToString()} unregistered");
            }
            else
            {
                Console.WriteLine($"Observer {client.ToString()} not found");
            }

        }
        catch (Exception e)
        {
            throw new Exception("Logout failed", e);
        }
        finally
        {
            RemoveObserver(client);
            //write the observer to console
            //print the remaining observers
            Console.WriteLine("Remaining observers:");
            foreach (var observer in observers)
            {
                Console.WriteLine(observer.ToString());
            }
        }

    }

    public void RegisterObserver(IObserver observer)
    {

    }

    public void RemoveObserver(IObserver observer)
    {
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers.Values)
        {
            Task.Run(() =>
            {
                try
                {
                    observer.Update();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error notifying observer {observer.ToString()}: {e.Message}");
                }
            });
        }
    }
}