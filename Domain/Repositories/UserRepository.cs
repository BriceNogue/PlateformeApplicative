using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shareds.Modeles;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Shareds.Modeles.ResponsesModels;

namespace Domain.Repositories
{
    public class UserRepository(DataContext _dataContext, UserManager<Utilisateur> _userManager, TypeRepository typeRepository, IConfiguration config)
    {
        public async Task<List<Utilisateur>> GetAll()
        {
            try
            {
                var res = await _dataContext.Utilisateurs.ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Utilisateur> Get(int id)
        {
            
            try
            {
                var res = await _dataContext.Utilisateurs.ToListAsync();
                return res.FirstOrDefault(x => x.Id == id)!;
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

        public async Task<LoginResponse> Login(UserLoginModele loginM)
        {
            var existUser = await _userManager.FindByEmailAsync(loginM.Email);
            if (existUser is null)
                return new LoginResponse(false, null, "Utilisateur inexistant");

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(existUser, loginM.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null, "Email ou mot de passe invalide");

            var userRole = typeRepository.Get(existUser.IdType);
            var userSession = new UserSession(existUser.Id, existUser.Nom, existUser.Email, userRole.Libelle);
            string token = GenerateToken(userSession);

            return new LoginResponse(true, token, "login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Ajouter le préfixe "Bearer" au token JWT (Norme OAuth 2.0) Mais Optionnel dans certains cas
            var tokenWithBearer = "Bearer " + jwtToken;

            return tokenWithBearer;
        }
    }

}
