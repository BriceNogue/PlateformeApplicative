
namespace Domain.Entities
{
    public class Poste
    {
        public int Id { get; set; }
        public string LibellePoste { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string AdresseIp { get; set; } = string.Empty;
        public string AdresseMAC { get; set; } = string.Empty;
        public int IdSalle { get; set; }
        public int IdType { get; set; }
        public bool Statut { get; set; }

        public TypeE TypeE { get; set; } = default!;
        public Salle Salle { get; set; } = default!;
    }
}
