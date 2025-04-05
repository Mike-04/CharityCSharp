namespace Charity.Network.ObjectProtocol;
[Serializable]
public class AddCazCaritabilRequest :IRequest
{
    public string Nume { get; set; }
    public double Suma { get; set; }

    public AddCazCaritabilRequest(string nume, double suma)
    {
        Nume = nume;
        Suma = suma;
    }
}