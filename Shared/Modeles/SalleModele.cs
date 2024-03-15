using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class SalleModele
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacite { get; set; }
        public bool Status { get; set; }
        public int IdType { get; set; }
        public int IdEtablissement { get; set; }
    }
}
