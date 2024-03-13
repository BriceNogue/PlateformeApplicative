using Shareds.Modeles;

namespace Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "";

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
    }
}
