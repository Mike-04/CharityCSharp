namespace Charity.Domain;
public class Entity<T>
{
    public T Id { get; set; }

    public Entity(T Id)
    {
        this.Id = Id;
    }

    public T GetId()
    {
        return Id;
    }

    public void SetId(T id)
    {
        Id = id;
    }
    
    
}