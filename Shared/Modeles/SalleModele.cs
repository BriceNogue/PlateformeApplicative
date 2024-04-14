using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class SalleModele
    {
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }
        [Required]
        public int Capacite { get; set; }
        public bool Status { get; set; }
        [Required]
        public int IdType { get; set; }
        public int IdEtablissement { get; set; }
    }
}
