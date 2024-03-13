using Shareds.Modeles;

namespace Web.Services
{
    public class ParcService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/etablissements";

        public ParcService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<EtablissementModele>> GetAll()
        {
            List<EtablissementModele>? parcs = await _httpClient.GetFromJsonAsync<List<EtablissementModele>>(_URL + "/all");

            if (parcs != null)
            {
                return parcs;
            }
            else
            {
                return new List<EtablissementModele>();
            }
        }
    }
}
