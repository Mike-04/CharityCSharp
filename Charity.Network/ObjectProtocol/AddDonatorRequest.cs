namespace Charity.Network.ObjectProtocol;
[Serializable]
public class AddDonatorRequest :IRequest
{
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public string NumarTelefon { get; set; }

    public AddDonatorRequest(string nume, string adresa, string numarTelefon)
    {
        Nume = nume;
        Adresa = adresa;
        NumarTelefon = numarTelefon;
    }
}