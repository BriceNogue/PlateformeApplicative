using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class EtablissementModele
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Pays { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Ville { get; set; } = string.Empty;

        [Required]
        public int NumeroRue { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LibelleRue { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; }
    }
}
