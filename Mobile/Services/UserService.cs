using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;
using Newtonsoft.Json;
using Shareds.Modeles;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static Shareds.Modeles.ResponsesModels;
using UserSession = Mobile.Domain.Entities.UserSession;

namespace Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/users";

        //UserSessionRepository userSR;
        //public static Domain.Entities.UserSession userSession = null;

        public UserService() 
        {
            //userSR = new UserSessionRepository();
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _httpClient  = new HttpClient();
#endif

        }

        // Pour addapter le contenu de la requete
        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<LoginResponse> Register(UserModele user)
        {
            var content = SetRequestContent(user);
            HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/register", content);

            string responseBody = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;

            return res;
        }

        public async Task<LoginResponse> Login(UserLoginModele loginM)
        {
            try
            {
                StringContent content = (StringContent)SetRequestContent(loginM);

                HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/login", content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var userLogin = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;

                if (userLogin.Flag)
                {
                    if (userLogin.Token!.StartsWith("Bearer "))
                    {
                        //string userToken = userLogin.Token!.Substring("Bearer ".Length);
                        //SaveUserToken(userToken);
                    }
                    else
                    {
                        //SaveUserToken(userLogin.Token);
                    }
                }

                return userLogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Récupère le Token à la connexion et crée une userSession
        public UserSession SetUserSession(string token)
        {
            UserSession userSession = new UserSession();

            if (token is not null)
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring("Bearer ".Length);
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

                    userSession = new UserSession()
                    {
                        UserId = int.Parse(id),
                        Name = name,
                        Email = email,
                        Role = role,
                        Token = token,
                    };
                    string jsonUserS = JsonConvert.SerializeObject(userSession);
                }
            }

            return userSession;
        }

        public void SaveUserPreferences(string token)
        {
            UserSession userSession = new UserSession();
            userSession = SetUserSession(token);

            string jsonUserSession = JsonConvert.SerializeObject(userSession);
            Preferences.Default.Set("user_session", jsonUserSession);
        }

        public UserSession GetUserPreferences()
        {
            try
            {
                /*var res = await userSR.GetUserSession();

                if (res == null)
                {
                    return string.Empty;
                }
                else
                {
                    string token = res.Token;
                    return token;
                }*/

                UserSession userSession = null;
                bool haskey = Preferences.Default.ContainsKey("user_session");

                if (haskey)
                {
                    var jsonUserSession = Preferences.Default.Get("user_session", "{}");

                    userSession = new UserSession();
                    userSession = JsonConvert.DeserializeObject<UserSession>(jsonUserSession);
                }

                return userSession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserModele>> GetAllByParc(int parcId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/users_parc/id?id={parcId}");

                UserSession userSession = new UserSession();
                userSession = GetUserPreferences();
                string token = userSession.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
