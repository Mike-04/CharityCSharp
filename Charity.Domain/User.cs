namespace Charity.Domain;

public class User : Entity<Guid>
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public User(string username, string passwordHash) : base(Guid.NewGuid())
    {
        this.Username = username;
        this.PasswordHash = passwordHash;
    }

    public User(Guid Id, string username, string PasswordHash) : base(Id)
    {
        Username = username;
        this.PasswordHash = PasswordHash;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Username: {Username}, PasswordHash: {PasswordHash}";
    }
}