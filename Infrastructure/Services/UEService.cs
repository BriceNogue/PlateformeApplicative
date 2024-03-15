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
    public class UEService
    {
        private readonly UERepository _ueRepository;
        private readonly UserService _userService;
        private readonly EtablissementService _etablissementService;

        public UEService()
        {
            _ueRepository = new UERepository();
            _userService = new UserService();
            _etablissementService = new EtablissementService();
        }

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
            var user = _userService.Get(ue.IdUtilisateur);
            var etab = _etablissementService.Get(ue.IdEtablissement);
            if (ue.Id > 0 || user is null || etab is null)
            {
                return false;
            }
            else
            {
                var newUE = new UtilisateurEtablissement()
                {
                    Statut = ue.status,
                    DateCreation = DateTime.Now,
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
