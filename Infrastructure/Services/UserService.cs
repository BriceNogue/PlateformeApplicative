﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shareds.Modeles;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public async Task<List<UserModele>> GetAll()
        {
            var res = await _userRepository.GetAll();
            var data = new List<UserModele>();

            foreach (var item in res)
            {
                var user = new UserModele()
                {
                    Id = item.Id,
                    Nom = item.Nom,
                    Prenom = item.Prenom,
                    DateNaissance = item.DateNaissance,
                    PhoneNumber = item.PhoneNumber!,
                    Email = item.Email!,
                    IdType = item.IdType,
                    DateInscription = item.DateInscription,
                };

                data.Add(user);
            }

            return data;
        }

        public async Task<List<UserModele>> GetAllByParc(int id)
        {
            var res = _ueService.GetAll().Where(u => u.IdEtablissement == id).ToList();
            if (res.Count != 0)
            {
                var users = await GetAll();
                var data = new List<UserModele>();

                for (int i = 0; i < res.Count; i++)
                {
                    var user = users.FirstOrDefault(u => u.Id == res[i].IdUtilisateur); //await Get(res[i].IdUtilisateur);
                    if (user is not null)
                    {
                        data.Add(user);
                    }
                }

                return data;
            }
            else
            {
                return new List<UserModele>();
            }
        }

        public async Task<UserModele> Get(int id)
        {
            var res = await _userRepository.Get(id);

            if (res is not null)
            {
                var user = new UserModele()
                {
                    Id = res.Id,
                    Nom = res.Nom,
                    Prenom = res.Prenom,
                    DateNaissance = res.DateNaissance,
                    PhoneNumber = res.PhoneNumber!,
                    Email = res.Email!,
                    IdType = res.IdType,
                    DateInscription = res.DateInscription,
                };

                return user;
            }
            else
            {
                return null!;
            }
        }

        public async Task<UserModele> GetByMail(string mail)
        {
            var res = await GetAll();
            var users = res.FirstOrDefault(u => u.Email == mail);

            if (users != null)
            {
                return users;
            }
            else
            {
                return null!;
            }
        }

        public async Task<GeneralResponse> Register(UserModele user)
        {
            var users = await GetAll();
            
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
            var users = await GetAll();
            var etb = _etabRepository.Get(idParc);
            var type = _typeRepository.Get(user.IdType);

            if ((user.Id > 0) || etb is null || type is null)
            {
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
            else if (users.Any(x => (x.PhoneNumber == user.PhoneNumber))|| users.Any(x => (x.Email == user.Email)))
            {
                return new GeneralResponse(false, "Adress mail ou numéro de téléphone déja utilisé.");
            }
            else
            {
                var defaultPwd = user.Nom.ToUpper() + user.Prenom.ToLower() + "#" + 1234; // Mmmmdp automatique a la l'ajout d'un utilisateur,
                var newUser = new Utilisateur()
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    DateNaissance = user.DateNaissance,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    PasswordHash = defaultPwd,
                    DateInscription = DateTime.Now,
                    IdType = user.IdType,
                    UserName = user.Email
                };

                var res = await _userRepository.Add(newUser, defaultPwd);
                if (res.Flag)
                {
                    var addedUser = await GetByMail(newUser.Email);
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

        public async Task<GeneralResponse> Update(UserModele user)
        {
            var oldUser = await _userRepository.Get(user.Id);
            var type = _typeRepository.Get(user.IdType);
            if (oldUser is null || type is null)
            {
                return new GeneralResponse(false, "Mise a jour impossible.");
            }
            else
            {
                oldUser.Nom = user.Nom;
                oldUser.Prenom = user.Prenom;
                oldUser.DateNaissance = user.DateNaissance;
                oldUser.PhoneNumber = user.PhoneNumber;
                oldUser.Email = user.Email;
                oldUser.UserName = user.Email;
                oldUser.IdType = user.IdType;

                _userRepository.Update(oldUser);
                return new GeneralResponse(true, "Utilisateur mis a jour avec succes.");
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
