using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public class UEModele
    {
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdEtablissement { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
