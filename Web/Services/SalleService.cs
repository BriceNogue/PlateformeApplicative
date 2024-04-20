using Newtonsoft.Json;
using Shareds.Modeles;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Shareds.Modeles.ResponsesModels;

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
            if (UserService.userToken is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
            }

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

        public async Task<List<SalleModele>> GetAllByParc()
        {
            try
            {
                string token = UserService.userToken ?? "";
                int parcId = ParcService.parcSession!.Id;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                List<SalleModele>? salles = await _httpClient.GetFromJsonAsync<List<SalleModele>>(_URL + $"/all/id?id={parcId}");

                if (salles != null)
                {
                    return salles;
                }
                else
                {
                    return new List<SalleModele>();
                }
            }
            catch (Exception ex)
            {
                return null!;
            }

        }

        public async Task<SalleModele> GetById(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);

            var res = await _httpClient.GetFromJsonAsync<SalleModele>(_URL + $"/id?id={id}");

            if (res != null)
            {
                return res;
            }
            else
            {
                return null!;
            }

        }

        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<GeneralResponse> Create(SalleModele salle)
        {
            var content = SetRequestContent(salle);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);

            HttpResponseMessage res = await _httpClient.PostAsync(_URL + "/create", content);

            if (res.IsSuccessStatusCode)
            {
                return new GeneralResponse(true, "Salle créer avec succès.");
            }
            else
            {

                return new GeneralResponse(false, "Une erreur est survenue.");
            }
        }

    }
}
