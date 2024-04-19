using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Shareds.Modeles;

namespace Desktop.Services
{
    public class PosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/postes";

        private readonly DeviceInfoService _deviceInfoService;


        public PosteService()
        {
            _httpClient = new HttpClient();
            _deviceInfoService = new DeviceInfoService();
        }

        public async Task<List<PosteModele>> GetAllBySalle(int idSalle)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);

                HttpResponseMessage response = await _httpClient.GetAsync(_URL + $"/all/id?id={idSalle}");

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

        // Pour la verification automatique de l'existence du poste
        public async Task<PosteModele> GetOne()
        {
            PosteLoginModele posteLogin = new PosteLoginModele();
            posteLogin.MacAddress = _deviceInfoService.GetMACAddress();
            posteLogin.Manufacturer = _deviceInfoService.GetComputerManufacturer();

            try
            {
                string json = JsonConvert.SerializeObject(posteLogin);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/one", content);

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

        // Pour addapter le contenu de la requete
        private HttpContent SetRequestContent(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<bool> Add(PosteModele poste)
        {
            var content = SetRequestContent(poste);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
            HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/create", content);

            if (response.IsSuccessStatusCode)
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
