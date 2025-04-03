using Charity.Domain;
using Charity.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Service
{
    public class CazCaritabilService
    {
        private CazCaritabilIRepository cazCaritabilDBRepository;

        public CazCaritabilService(CazCaritabilIRepository cazCaritabilDBRepository)
        {
            this.cazCaritabilDBRepository = cazCaritabilDBRepository;
        }

        public void Add(string nume, double sumaAdunata)
        {
            cazCaritabilDBRepository.Add(new CazCaritabil(nume, sumaAdunata));
        }

        public void Remove(Guid id)
        {
            cazCaritabilDBRepository.Remove(id);
        }

        public void Update(Guid id,string nume, double sumaAdunata)
        {
            cazCaritabilDBRepository.Update(new CazCaritabil(nume, sumaAdunata) { Id = id });
        }

        public List<CazCaritabil> GetAll()
        {
            return cazCaritabilDBRepository.GetAll().ToList();
        }

    }
}
