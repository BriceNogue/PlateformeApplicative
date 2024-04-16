
using System.ComponentModel.DataAnnotations;

namespace Shareds.Modeles
{
    public class UserModele
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password, ErrorMessage ="Le mot de passe doit contenir des Az@#12")]
        public string MotDePasse { get; set; } = string.Empty;

        [DataType(DataType.Password, ErrorMessage ="Les mots de passe ne correspondent pas.")]
        [Compare(nameof(MotDePasse))]
        public string ConfirmeMotDePasse { get; set; } = string.Empty;

        public DateTime DateInscription { get; set; }
        public int IdType { get; set; }
    }
}
