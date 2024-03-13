
namespace Domain.Entities
{
    public class Etablissement
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public string CodePostal { get; set; } = string.Empty;
        public string Pays { get; set; } = string.Empty;

        public List<Salle> Salles { get; set; } = default!;
        //public List<User> Users { get; set; } = default!;
    }
}
