using Charity.Domain;

namespace Charity.Network.DTO;
[Serializable]
public class DonatorDTO : EntityDTO<Guid>
{
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public string NumarTelefon { get; set; }
    
    public DonatorDTO(Guid id, string nume, string adresa, string numarTelefon) : base(id)
    {
        Nume = nume;
        Adresa = adresa;
        NumarTelefon = numarTelefon;
    }

    public static DonatorDTO FromDonator(Donator donator)
    {
        return new DonatorDTO(donator.Id, donator.Nume,donator.Adresa, donator.NumarTelefon);
    }
    
    public Donator ToDonator()
    {
        return new Donator(Id,Nume, Adresa, NumarTelefon);
    }
}