using Charity.Domain;

namespace Charity.Network.DTO;
[Serializable]
public class CazCaritabilDTO : EntityDTO<Guid>
{
    public string Nume { get; set; }
    public double SumaAdunata { get; set; }
    
    public CazCaritabilDTO(Guid id, string nume, double sumaAdunata) : base(id)
    {
        Nume = nume;
        SumaAdunata = sumaAdunata;
    }
    
    public static CazCaritabilDTO FromCazCaritabil(CazCaritabil cazCaritabil)
    {
        return new CazCaritabilDTO(cazCaritabil.Id, cazCaritabil.Nume, cazCaritabil.SumaAdunata);
    }

    public CazCaritabil ToCazCaritabil()
    {
        return new CazCaritabil(Id, Nume, SumaAdunata);
    }
    
}