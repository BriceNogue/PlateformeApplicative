using Domain.Entities;
using Domain.Repositories;
using Shareds.Modeles;

namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly TypeRepository _typeRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
            _typeRepository = new TypeRepository();
        }

        public List<Utilisateur> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Utilisateur Get(int id)
        {
            return _userRepository.Get(id);
        }

        public bool Add(UserModele user)
        {
            var users = GetAll();
            var userType = _typeRepository.Get(user.IdType);
            if ((user.Id > 0) || users.Any(x => (x.Email == user.Email) || x.Telephone == user.Telephone) || userType is null)
            {
                return false;
            }
            else
            {
                var newUser = new Utilisateur()
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    DateNaissance = user.DateNaissance,
                    Telephone = user.Telephone,
                    Email = user.Email,
                    MotDePasse = user.MotDePasse,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType
                };
                _userRepository.Add(newUser);
                return true;
            }
        }

        public bool Delete(int id)
        {
            var existingUser = _userRepository.Get(id);
            if (existingUser is null)
            {
                return false;
            }
            else
            {
                _userRepository.Delete(id);
                return true;
            }
        }

        public bool Update(UserModele user)
        {
            var oldUser = _userRepository.Get(user.Id);
            if (oldUser is null)
            {
                return false;
            }
            else
            {
                oldUser.Nom = user.Nom;
                oldUser.Prenom = user.Prenom;
                oldUser.DateNaissance = user.DateNaissance;
                oldUser.Telephone = user.Telephone;
                oldUser.Email = user.Email;
                oldUser.MotDePasse = user.MotDePasse;
                oldUser.IdType = user.IdType;

                _userRepository.Update(oldUser);
                return true;
            }
        }
    }
}
