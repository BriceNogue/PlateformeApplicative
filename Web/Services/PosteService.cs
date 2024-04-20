using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;

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
            var res = await _httpClient.GetAsync(_URL + "/all");
            
            if (res.IsSuccessStatusCode) 
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<PosteModele>>(jsonString);

                return data!;
            }
            else
            {
                return new List<PosteModele>();
            }

        }

        public async Task<List<PosteModele>> GetAllByParc()
        {
            try
            {
                int parcId = ParcService.parcSession!.Id;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);

                HttpResponseMessage response = await _httpClient.GetAsync(_URL + $"/all/id?id={parcId}");

                response.EnsureSuccessStatusCode(); // Pour s'assurer que la requete s'est terminee avec succes

                string responseBody = await response.Content.ReadAsStringAsync(); // Pour lire le contenu de la réponse

                List<PosteModele> allPostes = new List<PosteModele>();
                allPostes = JsonConvert.DeserializeObject<List<PosteModele>>(responseBody)!;

                return allPostes;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<PosteModele> GetById(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
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
