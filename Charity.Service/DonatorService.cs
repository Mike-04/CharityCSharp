using Charity.Domain;
using Charity.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Service
{
    public class DonatorService
    {
        private DonatorIRepository donatorRepository;
        public DonatorService(DonatorIRepository donatorDBRepository)
        {
            this.donatorRepository = donatorDBRepository;
        }

        public void Add(string nume, string adresa, string telefon)
        {
            donatorRepository.Add(new Donator(nume,  adresa, telefon));
        }

        public void Remove(Guid id)
        {
            donatorRepository.Remove(id);
        }

        public void Update(Guid id, string nume, string adresa, string telefon)
        {
            donatorRepository.Update(new Donator(nume, adresa, telefon) { Id = id });
        }

        public List<Donator> GetAll()
        {
            return donatorRepository.GetAll().ToList();
        }

        internal List<Donator> FindByName(string text)
        {
            return donatorRepository.FindByName(text).ToList();
        }
    }
}
