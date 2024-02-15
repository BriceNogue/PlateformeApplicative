using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modeles
{
    public class PosteModele
    {
        public int Id { get; set; }
        public string LibellePoste { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string AdresseIp { get; set; } = string.Empty;
        public string AdresseMAC { get; set; } = string.Empty;
        public string SE { get; set; } = string.Empty;
        public double ROM { get; set; }
        public double RAM { get; set; }
        public int IdSalle { get; set; }
        public int IdType { get; set; }
        public bool Statut { get; set; }
    }
}
