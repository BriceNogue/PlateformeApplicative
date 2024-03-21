using Azure;
using Domain.Entities;
using Domain.Migrations;
using Domain.Repositories;
using Shareds.Modeles;

namespace Infrastructure.Services
{
    public class SalleService
    {
        private readonly SalleRepository _repository;
        private readonly EtablissementRepository _etablissementRepository;
        private readonly TypeRepository _typeRepository;

        public SalleService() 
        {
            _repository = new SalleRepository();
            _etablissementRepository = new EtablissementRepository();
            _typeRepository = new TypeRepository();
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
            var etab = _etablissementRepository.Get(salle.IdEtablissement);
            var type = _typeRepository.Get(salle.IdType);
            if (salle.Id > 0 || etab is null || type is null || salle.Numero <= 0)
            {
                return false;
            }
            else
            {
                var newSalle = new Salle()
                {
                    Numero = salle.Numero,
                    Capacite = salle.Capacite,
                    Status = salle.Status,
                    IdType = salle.IdType,
                    IdEtablissement = salle.IdEtablissement
                };
                _repository.Add(newSalle);
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
                salleUpdated.Numero = salle.Numero;
                salleUpdated.Capacite = salle.Capacite;
                salleUpdated.Status = salle.Status;
                salleUpdated.IdType = salle.IdType;

                _repository.Update(salleUpdated);
                return true;
            }
        }
    }
}
