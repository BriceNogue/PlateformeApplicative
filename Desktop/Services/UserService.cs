using Newtonsoft.Json;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Shareds.Modeles.ResponsesModels;

namespace Desktop.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/users";

        //public static UserModele user;

        public static UserSession? userSession;
        public static string? userToken;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> Login(UserLoginModele loginM)
        {
            try
            {
                string json = JsonConvert.SerializeObject(loginM);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/login", content);

                response.EnsureSuccessStatusCode(); // Pour s'assurer que la requete s'est terminee avec succes

                string responseBody = await response.Content.ReadAsStringAsync(); // Pour lire le contenu de la réponse

                var userLogin = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;

                if (userLogin.Flag)
                {
                    SetUserSession(userLogin.Token!);
                }

                return userLogin;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        private void SetUserSession(string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
                userToken = token;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDecoded = tokenHandler.ReadJwtToken(token);

            // Accès aux revendications (données) du token JWT et èxtraction des infos
            var claims = tokenDecoded.Claims;

            if (claims is not null)
            {
                string name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!;
                string email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
                string role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!;
                string id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

                userSession = new UserSession(int.Parse(id), name, email, role);
            }
        }

    }
}
