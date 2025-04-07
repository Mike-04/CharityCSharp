using Charity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charity.Service.Observer;

namespace Charity.Service
{
    public class Service : IAppService
    {
        public CazCaritabilService CazCaritabilService;
        public UserService UserService;
        public DonatorService DonatorService;
        public DonatieService DonatieService;
        public Service(UserService userService,CazCaritabilService cazCaritabilService, DonatorService donatorService, DonatieService donatieService)
        {
            this.UserService = userService;
            this.CazCaritabilService = cazCaritabilService;
            this.DonatorService = donatorService;
            this.DonatieService = donatieService;
        }

        public void addDonation(Donator selectedDonor, CazCaritabil selectedCase, double v)
        {
            //get the caz and add the donation to its value
            selectedCase.AdaugaDonatie(v);
            //update the caz in repository
            CazCaritabilService.Update(selectedCase.Id, selectedCase.Nume, selectedCase.SumaAdunata);
            //add the donation to the repository
            DonatieService.Add(selectedDonor, selectedCase, v);

        }

        public void AddCazCaritabil(string nume, double sumaAdunata)
        {
            CazCaritabilService.Add(nume, sumaAdunata);
        }

        public void UpdateCazCaritabil(Guid id, string nume, double sumaAdunata)
        {
            CazCaritabilService.Update(id, nume, sumaAdunata);
        }

        public void AddDonator(string nume, string adresa, string telefon)
        {
            DonatorService.Add(nume, adresa, telefon);
        }

        public void UpdateDonator(Guid id, string nume, string adresa, string telefon)
        {
            DonatorService.Update(id, nume, adresa, telefon);
        }

        public void Logout(String username, IObserver client = null)
        {
            return;
        }

        public User Login(string username, string password,IObserver client = null)
        {
            if(UserService.CheckUser(username, password))
            {
                return UserService.FindByUsername(username);
            }
            else
            {
                throw new Exception("Invalid username or password");
            }
            
        }

        public IEnumerable<CazCaritabil> GetAllCazuri()
        {
            return CazCaritabilService.GetAll();
        }

        public IEnumerable<Donator> GetDonators(string searchString)
        {
            return DonatorService.FindByName(searchString);
        }
    }
}
