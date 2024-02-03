using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Modeles;
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
            if (etab.Id > 0)
            {
                return false;
            }
            else
            {
                var newEtab = new Etablissement()
                {
                    Id = etab.Id,
                    Nom = etab.Nom,
                    Telephone = etab.Telephone,
                    Email = etab.Email,
                    Adresse = etab.Adresse,
                    Ville = etab.Ville,
                    CodePostal = etab.CodePostal,
                    Pays = etab.Pays
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
                etabUpdated.Adresse = etab.Adresse;
                etabUpdated.Ville = etab.Ville;
                etabUpdated.CodePostal = etab.CodePostal;
                etabUpdated.Pays = etab.Pays;

                _repository.Update(etabUpdated);
                return true;
            }
        }
    }
}
