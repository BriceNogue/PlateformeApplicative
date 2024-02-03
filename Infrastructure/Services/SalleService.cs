using Domain.Entities;
using Domain.Migrations;
using Domain.Repositories;
using Infrastructure.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SalleService
    {
        private readonly SalleRepository _repository;

        public SalleService() 
        {
            _repository = new SalleRepository();
        }

        public List<Salle> GetAll() 
        { 
            return _repository.GetAll(); 
        }

        public Salle Get(int id) 
        {
            return _repository.Get(id);
        }
        
        public bool Add(SalleModele salle)
        {
            if (salle.Id > 0)
            {
                return false;
            }
            else
            {
                var newSalle = new Salle()
                {
                    Id = salle.Id,
                    Nom = salle.Nom,
                    Emplacement = salle.Emplacement,
                    Capacite = salle.Capacite,
                    Status = salle.Status,
                    IdType = salle.IdType,
                    IdEtablissement = salle.IdEtablissement
                };
                return true;
            }
        }

        public bool Delete(int id)
        {
            var salle = _repository.Get(id);
            if(salle is null)
                return false;
            _repository.Delete(id);
            return true;
        }

        public bool Update(SalleModele salle)
        {
            var salleUpdated = Get(salle.Id);
            if (salleUpdated is null)
            {
                return false;
            }
            else
            {
                salleUpdated.Nom = salle.Nom;
                salleUpdated.Emplacement = salle.Emplacement;
                salleUpdated.Capacite = salle.Capacite;
                salleUpdated.Status = salle.Status;
                salleUpdated.IdType = salle.IdType;

                _repository.Update(salleUpdated);
                return true;
            }
        }
    }
}
