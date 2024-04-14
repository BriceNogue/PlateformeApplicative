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
        private readonly UEService _ueService;
        private readonly EtablissementRepository _etabRepository;

        public UserService(UserRepository userRepository,TypeRepository typeRepository, UEService ueService, EtablissementRepository etabRepository)
        {
            _userRepository = userRepository;
            _typeRepository = typeRepository;
            _ueService = ueService;
            _etabRepository = etabRepository;
        }

        public List<Utilisateur> GetAll()
        {
            return _userRepository.GetAll();
        }

        public List<Utilisateur> GetAllByParc(int id)
        {
            var res = _ueService.GetAll().Where(u => u.IdEtablissement == id).ToList();
            if (res.Count != 0)
            {
                var users = GetAll();
                var data = new List<Utilisateur>();

                data = users.Where(u => u.Id == res[0].IdUtilisateur).ToList();

                return data;
            }
            else
            {
                return new List<Utilisateur>();
            }
        }

        public Utilisateur Get(int id)
        {
            return _userRepository.Get(id);
        }

        public Utilisateur GetByMail(string mail)
        {
            var res = GetAll().FirstOrDefault(u => u.Email == mail);
            if(res != null)
            {
                return res;
            }
            else
            {
                return null!;
            }
        }

        public async Task<GeneralResponse> Register(UserModele user)
        {
            var users = GetAll();
            
            if ((user.Id > 0))
            {
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
            else if (users.Any(x => (x.PhoneNumber == user.PhoneNumber)))
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
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    PasswordHash = user.MotDePasse,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType,
                    UserName = user.Email
                };

                if (user.IdType == 0)
                {
                    var types = _typeRepository.GetAll();
                    var userType = types.FirstOrDefault(t => t.Libelle == "SuperAdmin");
                    newUser.IdType = userType!.Id;
                }
                var res = await _userRepository.Add(newUser, user.ConfirmeMotDePasse);
                return res;
            }
        }

        // Pour ajouter un utilisateur a un parc
        public async Task<GeneralResponse> Add(UserModele user, int idParc)
        {
            var users = GetAll();
            var etb = _etabRepository.Get(idParc);
            var type = _typeRepository.Get(user.IdType);

            if ((user.Id > 0) || etb is null || type is null)
            {
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
            else if (users.Any(x => (x.PhoneNumber == user.PhoneNumber)))
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
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    PasswordHash = user.MotDePasse,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType,
                    UserName = user.Email
                };

                var res = await _userRepository.Add(newUser, user.ConfirmeMotDePasse);
                if (res.Flag)
                {
                    var addedUser = GetByMail(newUser.Email);
                    if (addedUser != null)
                    {
                        UEModele neuUE = new UEModele()
                        {
                            IdEtablissement = idParc,
                            IdUtilisateur = addedUser.Id,
                            status = true,
                            DateCreation = DateTime.Now,
                        };

                        var isAdded = _ueService.Add(neuUE);
                    }
                }
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
                oldUser.PhoneNumber = user.PhoneNumber;
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
