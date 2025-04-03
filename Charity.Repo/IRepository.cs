using Charity.Domain;

namespace Charity.Repo;

public interface IRepository<Id, T> where T : Entity<Id>
{
    void Add(T entity);
    void Update(T entity);
    void Remove(Id id);
    T Find(Id id);
    IEnumerable<T> GetAll();
    
}