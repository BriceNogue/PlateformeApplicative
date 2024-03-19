using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shareds.Interfaces;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Shareds.Modeles.ResponsesModels;

namespace Infrastructure.Services
{
    public class AccountService(UserManager<Utilisateur> userManager, TypeService typeService, IConfiguration config) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAccount(UserModele userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new Utilisateur()
            {
                Nom = userDTO.Nom,
                Prenom = userDTO.Prenom,
                DateNaissance = userDTO.DateNaissance,
                PhoneNumber = userDTO.Telephone,
                Email = userDTO.Email,
                DateInscription = userDTO.DateInscription,
                //IdType = userDTO.IdType,
                PasswordHash = userDTO.MotDePasse,
                UserName = userDTO.Prenom +userDTO.Nom
                
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser, userDTO.MotDePasse);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            else
            {
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(UserLoginModele loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null, "Entrez les informations de connexion");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null, "Utilisateur inexistant");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null, "Email ou mot de passe invalide");

            var getUserRole = typeService.Get(1);
            var userSession = new UserSession(getUser.Id, getUser.Nom, getUser.Email, getUserRole.Libelle);
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
