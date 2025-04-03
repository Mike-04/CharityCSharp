namespace Charity.Domain;
public class Donator : Entity<Guid>
{
    public string Nume { get; set; }
    public string Adresa { get; set; }
    public string NumarTelefon { get; set; }

    public Donator(string nume, string adresa, string numarTelefon) : base(Guid.NewGuid())
    {
        Nume = nume;
        Adresa = adresa;
        NumarTelefon = numarTelefon;
    }
    
    public Donator(Guid id, string nume, string adresa, string numarTelefon) : base(id)
    {
        Nume = nume;
        Adresa = adresa;
        NumarTelefon = numarTelefon;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Nume: {Nume}, Adresa: {Adresa}, NumarTelefon: {NumarTelefon}";
    }
}