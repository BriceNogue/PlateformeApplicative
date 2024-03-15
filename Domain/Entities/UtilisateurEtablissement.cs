using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UtilisateurEtablissement
    {
        public int Id {  get; set; }
        public int IdUtilisateur { get; set; }
        public int IdEtablissement { get; set; }
        public DateTime DateCreation { get; set; }

        public Utilisateur Utilisateur { get; set; } = default!;
        public Etablissement Etablissement { get; set;} = default!;
    }
}
