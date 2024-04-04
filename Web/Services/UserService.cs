using Shareds.Modeles;
using static Shareds.Modeles.ResponsesModels;
using System.Text;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

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

        public async Task<List<UserModele>> GetAllByParc()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/users_parc");

            if (userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Beader", userToken);
                var res = await _httpClient.SendAsync(request);

                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<UserModele>>(jsonString);
                    return data!;
                }
                else
                {
                    return new List<UserModele>();
                }
            }
            else
            {
                return new List<UserModele>();
            }
        }

        // Pour addapter le contenu de la requete
        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<LoginResponse> Signin(UserModele user)
        {
            var content = SetRequestContent(user);
            HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/signin", content);

            string responseBody = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;
            
            return res;
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
                    userToken = userLogin.Token!;
                    //SetUserSession();
                }

                return userLogin;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public void SetUserSession()
        {
            if (userToken is not null)
            {
                if (userToken.StartsWith("Bearer "))
                {
                    string token = userToken.Substring("Bearer ".Length);
                    userToken = token;
                }

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDecoded = tokenHandler.ReadJwtToken(userToken);

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

        public static void Logout()
        {
            userToken = null;
            userSession = null;
            isLoginPage = true;
        }
    }
}
