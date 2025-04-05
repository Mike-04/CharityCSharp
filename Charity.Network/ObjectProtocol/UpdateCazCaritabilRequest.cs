namespace Charity.Network.ObjectProtocol;
[Serializable]
public class UpdateCazCaritabilRequest :IRequest
{
    public Guid Id { get; set; }
    public string Nume { get; set; }
    public double Suma { get; set; }

    public UpdateCazCaritabilRequest(Guid id,string nume, double suma)
    {
        Id = id;
        Nume = nume;
        Suma = suma;
    }
}