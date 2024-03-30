using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;

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

        public async Task<List<EtablissementModele>> GetAllByUser(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/all/id?id={id}");

            if (UserService.userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Beader", UserService.userToken);
                var res = await _httpClient.SendAsync(request);

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
            else
            {
                return new List<EtablissementModele>();
            }
        }
    }
}
