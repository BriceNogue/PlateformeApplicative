using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Salle
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Emplacement { get; set; } = string.Empty;
        public string Capacite { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int IdType { get; set; }
        public int IdEtablissement { get; set; }

        public TypeE TypeE { get; set; } = default!;
        public Etablissement Etablissement { get; set; } = default!;
    }
}
