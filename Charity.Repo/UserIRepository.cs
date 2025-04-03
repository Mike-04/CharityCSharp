using Charity.Domain;

namespace Charity.Repo
{
    public interface UserIRepository : IRepository<Guid, User>
    {
        public User FindByUsername(string username);
    }
}