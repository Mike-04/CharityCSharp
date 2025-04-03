using System.Security.Policy;
using Charity.Domain;

namespace Charity.Network.DTO;


[Serializable]
public class UserDTO : EntityDTO<Guid>
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }    
    
    public UserDTO(Guid id, string username, string passwordHash) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
    }
    
    public static UserDTO FromUser(User user)
    {
        return new UserDTO(user.Id, user.Username, user.PasswordHash);
    }

    public User ToUser()
    {
        return new User(Id, Username, PasswordHash);
    }
}