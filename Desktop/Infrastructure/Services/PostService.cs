using System.Net.Http;
using Newtonsoft.Json;
using Shared.Modeles;

namespace Desktop.Infrastructure.Services
{
    public class PosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/postes";

        public PosteService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<PosteModele> GetOne()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_URL+"/id");

                response.EnsureSuccessStatusCode(); // Pour s'assurer que la requete s'est terminee avec succes

                string responseBody = await response.Content.ReadAsStringAsync(); // Pour lire le contenu de la réponse

                PosteModele poste = new PosteModele();
                poste = JsonConvert.DeserializeObject<PosteModele>(responseBody)!;

                return poste;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
