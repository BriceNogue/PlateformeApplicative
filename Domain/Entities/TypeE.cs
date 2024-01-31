
namespace Domain.Entities
{
    public class TypeE
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ObjetConcerne { get; set; } = string.Empty;

        public List<User> Users { get; set; } = default!;
        public List<Salle> Salles { get; set; } = default!;
        public List<Poste> Postes { get; set; } = default!;
    }
}
