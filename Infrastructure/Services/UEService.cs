using Domain.Entities;
using Domain.Repositories;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UEService(UERepository _ueRepository)
    {
        public List<UtilisateurEtablissement> GetAll()
        {
            return _ueRepository.GetAll();
        }

        public UtilisateurEtablissement Get(int id)
        {
            return _ueRepository.Get(id);
        }

        public bool Add(UEModele ue)
        {
            if (ue.Id > 0)
            {
                return false;
            }
            else
            {
                var newUE = new UtilisateurEtablissement()
                {
                    Statut = ue.status,
                    DateCreation = ue.DateCreation,
                    IdUtilisateur = ue.IdUtilisateur,
                    IdEtablissement = ue.IdEtablissement,
                };

                _ueRepository.Add(newUE);
                return true;
            }
        }

        public bool Delete(int id)
        {
            var existingUE = _ueRepository.Get(id);
            if (existingUE is null)
                return false;
            _ueRepository.Delete(id);
            return true;
        }

        public bool Update(UEModele obj)
        {
            var oldObj = _ueRepository.Get(obj.Id);
            if (oldObj is null)
            {
                return false;
            }
            else
            {
                oldObj.Statut = obj.status;
                oldObj.IdUtilisateur = obj.IdUtilisateur;
                oldObj.IdEtablissement = obj.IdEtablissement;

                _ueRepository.Update(oldObj);
                return true;
            }
        }
    }
}
