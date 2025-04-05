namespace Charity.Network.ObjectProtocol;
[Serializable]
public class UpdateDonatorRequest :IRequest
{
    public Guid Id { get; set; }
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public string NumarTelefon { get; set; }

    public UpdateDonatorRequest(Guid id, string nume, string adresa, string numarTelefon)
    {
        Id = id;
        Nume = nume;
        Adresa = adresa;
        NumarTelefon = numarTelefon;
    }
    
}