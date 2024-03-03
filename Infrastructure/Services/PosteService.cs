using Domain.Entities;
using Domain.Repositories;
using Shared.Modeles;

namespace Infrastructure.Services
{
    public class PosteService
    {
        private readonly PosteRepository _repository;
        private readonly SalleRepository _salleRepository;
        private readonly TypeRepository _typeRepository;

        public PosteService() 
        {
            _repository = new PosteRepository();
            _salleRepository = new SalleRepository();
            _typeRepository = new TypeRepository();
        }

        public List<Poste> GetAll()
        { 
            return _repository.GetAll(); 
        }

        public Poste Get(int id)
        {
            return _repository.Get(id);
        }

        public Poste GetOne(PosteLoginModele posteLogin)
        {
            Poste poste = new Poste()
            {
                Marque = posteLogin.Manufacturer,
                AdresseMAC = posteLogin.MacAddress
            };
            return _repository.GetOne(poste);
        }

        public bool Add(PosteModele poste)
        {
            var salle = _salleRepository.Get(poste.IdSalle);
            var type = _typeRepository.Get(poste.IdType);
            if (poste.Id > 0 || salle is null || type is null)
            {
                return false;
            }
            else
            {
                var newPost = new Poste()
                {
                    LibellePoste = poste.LibellePoste,
                    Marque = poste.Marque,
                    AdresseIp = poste.AdresseIp,
                    AdresseMAC = poste.AdresseMAC,
                    SE = poste.SE,
                    ROM = poste.ROM,
                    RAM = poste.RAM,
                    IdSalle = poste.IdSalle,
                    IdType = poste.IdType,
                    Statut = false,
                };
                _repository.Add(newPost);
                return true;
            }
        }

        public bool Delete(int id)
        {
            var poste = Get(id);
            if (poste is null)
                return false;
            _repository.Delete(id);
            return true;
        }

        public bool Update(PosteModele poste)
        {
            var postUpdated = Get(poste.Id);
            var salle = _salleRepository.Get(poste.IdSalle);
            var type = _typeRepository.Get(poste.IdType);
            if (postUpdated is null || salle is null || type is null)
            {
                return false;
            }
            else
            {
                postUpdated.LibellePoste = poste.LibellePoste;
                postUpdated.Marque = poste.Marque;
                postUpdated.AdresseIp = poste.AdresseIp;
                postUpdated.AdresseMAC = poste.AdresseMAC;
                postUpdated.IdSalle = poste.IdSalle;
                postUpdated.IdType = poste.IdType;
                postUpdated.Statut = poste.Statut;

                _repository.Update(postUpdated);
                return true;
            }
        }
    }
}
