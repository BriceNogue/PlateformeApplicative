using Shareds.Modeles;
using static Shareds.Modeles.ResponsesModels;
using System.Text;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using System.Data;
using System.Xml.Linq;

namespace Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = default!;
        private readonly string _URL = "https://localhost:7281/api/users";

        //public static string? userToken;
        //public static UserSession? userSession;

        public static bool isLoginPage = true;

        private IJSRuntime JS;

        public UserService(IJSRuntime js)
        {
            JS = js;
            _httpClient = new HttpClient();
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
                string json = JsonConvert.SerializeObject(loginM);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/login", content);

                response.EnsureSuccessStatusCode(); // Pour s'assurer que la requete s'est terminee avec succes

                string responseBody = await response.Content.ReadAsStringAsync(); // Pour lire le contenu de la réponse

                var userLogin = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;

                if (userLogin.Flag)
                {
                    if (userLogin.Token!.StartsWith("Bearer "))
                    {
                        string userToken = userLogin.Token!.Substring("Bearer ".Length);
                        SaveUserToken(userToken);
                    }
                    else
                    {
                        SaveUserToken(userLogin.Token);
                    }
                }

                return userLogin;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        #region /// Token

        public async void SaveUserToken(string token)
        {
            try
            {
                await JS.InvokeVoidAsync("localStorage.setItem", "Token", token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<string> GetUserToken()
        {
            try
            {
                var res = await JS.InvokeAsync<string>("localStorage.getItem", "Token");
                if (res != null)
                {
                    return res;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveUserToken()
        {
            try
            {
                await JS.InvokeVoidAsync("localStorage.removeItem", "Token");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region /// User Session

        public async void SetUserSession()
        {
            string token = await GetUserToken();
            if (token is not null)
            {
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

                    UserSession userSession = new UserSession(int.Parse(id), name, email, role);
                    string jsonUserS = JsonConvert.SerializeObject(userSession);
                    await JS.InvokeVoidAsync("localStorage.setItem", "UserSession", jsonUserS);
                }
            }
        }

        public async Task<UserSession> GetUserSession()
        {
            try
            {
                var res = await JS.InvokeAsync<string>("localStorage.getItem", "UserSession");
                if (res != null)
                {
                    UserSession userS = JsonConvert.DeserializeObject<UserSession>(res)!;
                    return userS;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveUserSession()
        {
            try
            {
                await JS.InvokeVoidAsync("localStorage.removeItem", "UserSession");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        public async void Logout()
        {
            //userToken = null;
            //userSession = null;
            isLoginPage = true;

            await RemoveUserToken();
            await RemoveUserSession();

            //ParcService.parcSession = null;
        }

        public async Task<List<UserModele>> GetAllByParc(int parcId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/users_parc/id?id={parcId}");

                string token = await GetUserToken();

                if (token is not null)
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
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse> AddUser(UserModele user, int parcId)
        {
            try
            {
                var content = SetRequestContent(user);
                string token = await GetUserToken();

                if (content is not null && token is not null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                HttpResponseMessage res = await _httpClient.PostAsync(_URL + $"/add/id?id={parcId}", content);

                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<GeneralResponse>(jsonString);

                    return data!;
                }
                else
                {
                    return new GeneralResponse(false, "Une erreur est survenue.");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
