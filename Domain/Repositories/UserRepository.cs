using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using static Shareds.Modeles.ResponsesModels;

namespace Domain.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _dataContext;
        private UserManager<Utilisateur> _userManager;

        public UserRepository(UserManager<Utilisateur> userManager) 
        { 
            _dataContext = new DataContext();
            _userManager = userManager;
        }

        public List<Utilisateur> GetAll()
        {
            try
            {
                return _dataContext.Utilisateurs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Utilisateur Get(int id)
        {
            
            try
            {
                return _dataContext.Utilisateurs.FirstOrDefault(x => x.Id == id)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*public void Add(Utilisateur user)
        {
            try
            {
                _dataContext.Utilisateurs.Add(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }*/

        public async Task<GeneralResponse> Add(Utilisateur newUser, string userPWD)
        {
            try
            {
                var existUser = await _userManager.FindByEmailAsync(newUser.Email!);
                if (existUser is not null) return new GeneralResponse(false, "Adresse email déja utilisé.");

                var createUser = await _userManager.CreateAsync(newUser, userPWD);
                if (!createUser.Succeeded) return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");

                else
                {
                    return new GeneralResponse(true, "Compte crée avec succès");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var user = Get(id);
                _dataContext.Utilisateurs.Remove(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void Update(Utilisateur user)
        {
            try
            {
                _dataContext.Utilisateurs.Update(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }

}
