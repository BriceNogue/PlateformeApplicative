using Azure;
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
    public class EtablissementService(EtablissementRepository _repository, UEService _ueService, UserService _userService)
    {
        public List<Etablissement> GetAll()
        {
            return _repository.GetAll();
        }

        public Etablissement Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Etablissement> GetAllByUser(int id)
        {
            var res = _ueService.GetAll().Where(u => u.IdUtilisateur == id).ToList();
            if (res.Count != 0)
            {
                var etabs = GetAll();
                var data = new List<Etablissement>();

                for(int i = 0; i < res.Count; i++)
                {
                    var etab = etabs.FirstOrDefault(e => e.Id == res[i].IdEtablissement); //Get(res[i].IdEtablissement);
                    if (etab is not null)
                    {
                        data.Add(etab);
                    }
                }

                return data;
            }
            else
            {
                return new List<Etablissement>();
            }
        }

        public Etablissement GetByName(string name)
        {
            var res = _repository.GetAll().FirstOrDefault(e => e.Nom == name);
            if (res is not null)
            {
                return res;
            }
            else
            {
                return null!;
            }

        }

        public bool Add(EtablissementModele etab, int userId)
        {
            var user = _userService.Get(userId);
            if (user is null)
                return false;

            var etabs = GetAll();
            if (etab.Id > 0 || etabs.Any(e => e.Nom == etab.Nom) || etabs.Any(e => e.Email == etab.Email) || etabs.Any(e => e.Telephone == etab.Telephone))
            {
                return false;
            }
            else
            {
                var newEtab = new Etablissement()
                {
                    Nom = etab.Nom,
                    Telephone = etab.Telephone,
                    Email = etab.Email,
                    Pays = etab.Pays,
                    CodePostal = etab.CodePostal,
                    Ville = etab.Ville,
                    NumeroRue = etab.NumeroRue,
                    LibelleRue = etab.LibelleRue,
                    DateCreation = DateTime.Now,
                };
                var res = _repository.Add(newEtab);
                if (res)
                {
                    var addedEtab = GetByName(etab.Nom);
                    UEModele newUE = new UEModele()
                    {
                        IdUtilisateur = userId,
                        IdEtablissement = addedEtab.Id,
                        DateCreation = addedEtab.DateCreation,
                        status = true
                    };
                    var isUEAdded = _ueService.Add(newUE);
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool Delete(int id)
        {
            var etab = _repository.Get(id);
            if (etab is null)
                return false;
            _repository.Delete(id);
                return true;
        }

        public bool Update(EtablissementModele etab)
        {
            var etabUpdated = _repository.Get(etab.Id);
            if (etabUpdated is null)
            {
                return false;
            }
            else
            {
                etabUpdated.Nom = etab.Nom;
                etabUpdated.Telephone = etab.Telephone;
                etabUpdated.Email = etab.Email;
                etabUpdated.Pays = etab.Pays;
                etabUpdated.CodePostal = etab.CodePostal;
                etabUpdated.Ville = etab.Ville;
                etabUpdated.NumeroRue = etab.NumeroRue;
                etabUpdated.LibelleRue = etab.LibelleRue;

                _repository.Update(etabUpdated);
                return true;
            }
        }
    }
}
