using Newtonsoft.Json;
using Shareds.Modeles;

namespace Web.Services
{
    public class UEService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/etilisateursE";

        public UEService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<UEModele>> GetAll()
        {
            var res = await _httpClient.GetAsync(_URL + "/all");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<UEModele>>(jsonString);
                return data!;
            }
            else
            {
                return new List<UEModele>();
            }

        }

        public async Task<UEModele> GetById(int id)
        {
            var res = await _httpClient.GetAsync(_URL + $"/id?id={id}");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UEModele>(jsonString);
                return data!;
            }
            else
            {
                return null!;
            }

        }

        public async Task<List<UEModele>> GetAllByUserId(int id)
        {
            var res = await _httpClient.GetAsync(_URL + $"/all/id?id={id}");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<UEModele>>(jsonString);
                return data!;
            }
            else
            {
                return new List<UEModele>();
            }

        }
    }
}
