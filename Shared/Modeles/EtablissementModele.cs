using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class EtablissementModele
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pays { get; set; } = string.Empty;
        public string CodePostal { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public int NumeroRue { get; set; }
        public string LibelleRue { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; }
    }
}
