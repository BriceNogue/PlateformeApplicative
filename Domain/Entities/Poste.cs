
namespace Domain.Entities
{
    public class Poste
    {
        public int Id { get; set; }
        public string LibellePoste { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string AdresseIp { get; set; } = string.Empty;
        public string AdresseMAC { get; set; } = string.Empty;
        public string IdSalle { get; set; } = string.Empty;
        public string IdType { get; set; } = string.Empty;
        public string Statut { get; set; } = string.Empty;

        public TypeE TypeE { get; set; } = default!;
    }
}
