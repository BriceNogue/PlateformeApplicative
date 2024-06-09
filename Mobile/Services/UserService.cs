using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;
using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;
using System.Text;
using static Shareds.Modeles.ResponsesModels;

namespace Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/users";

        UserSessionRepository userSR;
        public static Domain.Entities.UserSession userSession = null;

        public UserService() 
        {
            userSR = new UserSessionRepository();
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
                        string userToken = userLogin.Token!.Substring("Bearer ".Length);
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

        public async Task<string> GetUserToken()
        {
            try
            {
                var res = await userSR.GetUserSession();

                if (res == null)
                {
                    return string.Empty;
                }
                else
                {
                    string token = res.Token;
                    return token;
                }
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
