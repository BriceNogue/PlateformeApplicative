using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class PosteModele
    {
        public int Id { get; set; }

        [Range(1,1000)]
        public int Numero { get; set; }
        public string Marque { get; set; } = string.Empty;
        public string AdresseIP { get; set; } = string.Empty;
        public string AdresseMAC { get; set; } = string.Empty;
        public string SE { get; set; } = string.Empty;
        public double ROM { get; set; }
        public double RAM { get; set; }
        public int IdSalle { get; set; }
        public int IdType { get; set; }
        public bool Statut { get; set; }
    }
}
