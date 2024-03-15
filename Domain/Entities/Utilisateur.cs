using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public DateTime DateInscription { get; set; }
        public int IdType { get; set; }

        public TypeE TypeE { get; set; } = default!;

        public List<UtilisateurEtablissement> UtilisateurEtabs { get; set; } = default!;
    }
}
