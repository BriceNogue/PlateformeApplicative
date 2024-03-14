using Shareds.Modeles;
using System.Text;

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

        public async Task<SalleModele> GetById(int id)
        {
            SalleModele? salle = await _httpClient.GetFromJsonAsync<SalleModele>(_URL + $"/id?id={id}");

            if (salle != null)
            {
                return salle;
            }
            else
            {
                return new SalleModele();
            }

        }

        public async Task<bool> Create(SalleModele salle)
        {
            HttpResponseMessage res = await _httpClient.PostAsJsonAsync(_URL, salle);
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
