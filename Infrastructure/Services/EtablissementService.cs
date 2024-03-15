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
    public class EtablissementService
    {
        private readonly EtablissementRepository _repository;

        public EtablissementService() 
        {
            _repository = new EtablissementRepository();
        }

        public List<Etablissement> GetAll()
        {
            return _repository.GetAll();
        }

        public Etablissement Get(int id)
        {
            return _repository.Get(id);
        }

        public bool Add(EtablissementModele etab)
        {
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
                _repository.Add(newEtab);
                return true;          
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
