namespace Charity.Network.DTO;


[Serializable]
public class EntityDTO<T>
{
        public T Id { get; set; }

        public EntityDTO(T id)
        {
            Id = id;
        }
}