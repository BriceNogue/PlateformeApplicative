using Azure;
using Domain.Entities;
using Domain.Migrations;
using Domain.Repositories;
using Shareds.Modeles;

namespace Infrastructure.Services
{
    public class SalleService(SalleRepository _repository, TypeRepository _typeRepository, EtablissementRepository _etablissementRepository)
    {
        public List<Salle> GetAll() 
        { 
            return _repository.GetAll(); 
        }

        public Salle Get(int id) 
        {
            return _repository.Get(id);
        }

        public List<Salle> GetAllByParc(int idEtab)
        {
            var res = _repository.GetAll().Where(e => e.IdEtablissement == idEtab).ToList();
            return res;
        }

        public bool Add(SalleModele salle)
        {
            var etab = _etablissementRepository.Get(salle.IdEtablissement);
            var hasSameNumber = GetAllByParc(salle.IdEtablissement).Any(s => s.Numero == salle.Numero);
            var type = _typeRepository.Get(salle.IdType);
            if (salle.Id > 0 || etab is null || type is null || salle.Numero <= 0 || hasSameNumber)
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
