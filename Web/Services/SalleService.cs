using Shareds.Modeles;

namespace Web.Services
{
    public class SalleService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/salles";

        public SalleService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<SalleModele>> GetAll()
        {
            List<SalleModele>? salles = await _httpClient.GetFromJsonAsync<List<SalleModele>>(_URL + "/all");

            if (salles != null)
            {
                return salles;
            }
            else
            {
                return new List<SalleModele>();
            }

        }
    }
}
