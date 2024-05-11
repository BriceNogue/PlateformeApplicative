using Newtonsoft.Json;
using Shareds.Modeles;
using System.Text;
using static Shareds.Modeles.ResponsesModels;

namespace Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = default!;
        private readonly string _URL = "https://localhost:7281/api/users";

        public UserService() 
        {
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
                var userLogin = new LoginResponse(false, null, string.Empty);

                StringContent content = (StringContent)SetRequestContent(loginM);

                HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/login", content);

                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    userLogin = JsonConvert.DeserializeObject<LoginResponse>(responseBody)!;

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
                }

                return userLogin;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
