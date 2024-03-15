using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class TypeModele
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Objet { get; set; } = string.Empty;
    }
}
