using Charity.Domain;
using Charity.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Service
{
    public class DonatieService
    {
        private DonatieIRepository _donatieRepository;
        public DonatieService(DonatieIRepository donatieIRepository) {
            _donatieRepository = donatieIRepository;
        }

        public void Add(Donator donator,CazCaritabil cazCaritabil,double suma)
        {
            Donatie donatie = new Donatie(donator, cazCaritabil, suma);
            _donatieRepository.Add(donatie);
        }

        public void Remove(Guid id)
        {
            _donatieRepository.Remove(id);
        }

        public List<Donatie> GetAll()
        {
            return _donatieRepository.GetAll().ToList();
        }

        public Donatie Find(Guid id)
        {
            return _donatieRepository.Find(id);
        }

        public void Update(Guid id, Donator donator, CazCaritabil cazCaritabil, double suma)
        {
            Donatie donatie = new Donatie(donator, cazCaritabil, suma) { Id = id };
            _donatieRepository.Update(donatie);
        }




    }
}
