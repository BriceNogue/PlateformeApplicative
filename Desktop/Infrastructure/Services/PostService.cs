using System.Net.Http;
using Newtonsoft.Json;
using Infrastructure.Modeles;

namespace Desktop.Infrastructure.Services
{
    public class PosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "url";

        public PosteService() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<PosteModele> Get()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_URL);

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
