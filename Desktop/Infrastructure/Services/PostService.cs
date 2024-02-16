using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Shared.Modeles;

namespace Desktop.Infrastructure.Services
{
    public class PosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/postes";
        private readonly string _URL_TYPES = "https://localhost:7281/api/types/all";

        private readonly DeviceInfoService _deviceInfoService;
        PosteLoginModele posteLogin = new PosteLoginModele();

        public PosteService()
        {
            _httpClient = new HttpClient();
            _deviceInfoService = new DeviceInfoService();

            posteLogin.MacAddress = _deviceInfoService.GetMACAddress();
            posteLogin.IpAddress = _deviceInfoService.GetIPAddress();
        }

        public async Task<List<TypeModele>> GetTypes()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_URL_TYPES);

                response.EnsureSuccessStatusCode(); // Pour s'assurer que la requete s'est terminee avec succes

                string responseBody = await response.Content.ReadAsStringAsync(); // Pour lire le contenu de la réponse

                List<TypeModele> types = new List<TypeModele>();
                types = JsonConvert.DeserializeObject<List<TypeModele>>(responseBody)!;

                return types;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<PosteModele> GetOne()
        {
            
            try
            {
                string json = JsonConvert.SerializeObject(posteLogin);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_URL+"/one", content);

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
        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<bool> Add(PosteModele poste)
        {
            var content = SetRequestContent(poste);

            HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/Add", content);
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
