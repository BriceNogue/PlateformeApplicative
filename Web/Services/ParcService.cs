using Newtonsoft.Json;
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
            var res = await _httpClient.GetAsync(_URL + "/all");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<EtablissementModele>>(jsonString);

                return data!;
            }
            else
            {
                return new List<EtablissementModele>();
            }
        }
    }
}
