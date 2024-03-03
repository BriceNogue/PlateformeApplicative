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
        public int Capacite { get; set; }
        public bool Status { get; set; }
        public int IdType { get; set; }
        public int IdEtablissement { get; set; }

        public TypeE TypeE { get; set; } = default!;
        public Etablissement Etablissement { get; set; } = default!;

        public List<Poste> Postes { get; set; } = default!;
    }
}
