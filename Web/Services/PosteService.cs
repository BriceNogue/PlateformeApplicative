using Shareds.Modeles;

namespace Web.Services
{
    public class PosteService : IPosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/postes";

        public PosteService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<PosteModele>> GetAll()
        {
            List<PosteModele>? postes = await _httpClient.GetFromJsonAsync<List<PosteModele>>(_URL + "/all");
            
            if (postes != null) 
            {
                return postes;
            }
            else
            {
                return new List<PosteModele>();
            }

        }
    }
}
