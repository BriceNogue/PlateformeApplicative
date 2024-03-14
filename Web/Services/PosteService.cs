using Shareds.Modeles;

namespace Web.Services
{
    public class PosteService
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

        public async Task<PosteModele> GetById(int id)
        {
            PosteModele? poste = await _httpClient.GetFromJsonAsync<PosteModele>(_URL + $"/id?id={id}");

            if (poste != null)
            {
                return poste;
            }
            else
            {
                return new PosteModele();
            }

        }
    }
}
