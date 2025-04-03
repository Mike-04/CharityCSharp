using Charity.Domain;

namespace Charity.Repo;

public interface DonatorIRepository : IRepository<Guid, Donator>
{
    public IEnumerable<Donator> FindByName(string nume);

}