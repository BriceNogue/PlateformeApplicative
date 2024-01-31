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
        public string Status { get; set; } = string.Empty;
        public string IdType { get; set; } = string.Empty;
        public string IdEtablissement { get; set; } = string.Empty;
    }
}
