using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shareds.Modeles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Shareds.Modeles.ResponsesModels;

namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly TypeRepository _typeRepository;

        public UserService(UserRepository userRepository,TypeRepository typeRepository)
        {
            _userRepository = userRepository;
            _typeRepository = typeRepository;
        }

        public List<Utilisateur> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Utilisateur Get(int id)
        {
            return _userRepository.Get(id);
        }

        /*public bool Add(UserModele user)
        {
            var users = GetAll();
            var userType = _typeRepository.Get(user.IdType);
            if ((user.Id > 0) || users.Any(x => (x.Email == user.Email) || x.PhoneNumber == user.Telephone) || userType is null)
            {
                return false;
            }
            else
            {
                string hashedPWD = HashPassword(user.MotDePasse);
                var newUser = new Utilisateur()
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    DateNaissance = user.DateNaissance,
                    PhoneNumber = user.Telephone,
                    Email = user.Email,
                    PasswordHash = hashedPWD,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType
                };
                _userRepository.Add(newUser);
                return true;
            }
        }*/


        public async Task<GeneralResponse> Add(UserModele user)
        {
            var users = GetAll();
            var userType = _typeRepository.Get(user.IdType);
            if ((user.Id > 0) || userType is null)
            {
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
            else if (users.Any(x => (x.PhoneNumber == user.Telephone)))
            {
                return new GeneralResponse(false, "Numéro de téléphone déja utilisé.");
            }
            else
            {
                var newUser = new Utilisateur()
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    DateNaissance = user.DateNaissance,
                    PhoneNumber = user.Telephone,
                    Email = user.Email,
                    PasswordHash = user.MotDePasse,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType,
                    UserName = user.Email
                };
                var res = await _userRepository.Add(newUser, user.MotDePasse);
                return res;
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
                oldUser.PhoneNumber = user.Telephone;
                oldUser.Email = user.Email;
                oldUser.IdType = user.IdType;

                _userRepository.Update(oldUser);
                return true;
            }
        }

        public async Task<LoginResponse> Login(UserLoginModele loginM)
        {
            if (loginM == null)
                return new LoginResponse(false, null, "Entrez les informations de connexion");

            var res = await _userRepository.Login(loginM);
            return res;
        }

        
        
    }
}
