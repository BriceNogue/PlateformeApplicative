
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class TypeE : IdentityRole<int>
    {
        //public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Objet { get; set; } = string.Empty;

        public List<Utilisateur> Utilisateurs { get; set; } = default!;
        public List<Salle> Salles { get; set; } = default!;
        public List<Poste> Postes { get; set; } = default!;
    }
}
