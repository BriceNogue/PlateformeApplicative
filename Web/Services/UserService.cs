using Shareds.Modeles;
using static Shareds.Modeles.ResponsesModels;
using System.Text;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/users";

        
        public static string? userToken;
        public static UserSession? userSession;

        public static bool isLoginPage = true;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<UserModele>> GetAll()
        {
            List<UserModele>? res = await _httpClient.GetFromJsonAsync<List<UserModele>>(_URL + "/all");
            if (res is not null)
            {
                return res;
            }
            else
            {
                return new List<UserModele>();
            }
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

        public void SetUserSession(string token)
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

        public static void Logout()
        {
            userToken = null;
            userSession = null;
            isLoginPage = true;
        }
    }
}
