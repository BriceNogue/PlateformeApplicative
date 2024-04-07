using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;
using static Shareds.Modeles.ResponsesModels;
using System.Text;

namespace Web.Services
{
    public class ParcService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/etablissements";

        public static ParcSession? parcSession;

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
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
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

        public async Task <EtablissementModele> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/id?id={id}");

            if (UserService.userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
                var res = await _httpClient.SendAsync(request);

                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EtablissementModele>(jsonString);
                    return data!;
                }
                else
                {
                    return new EtablissementModele();
                }
            }
            else
            {
                return new EtablissementModele();
            }
        }

        // Pour addapter le contenu de la requete
        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<GeneralResponse> Create(EtablissementModele etab, int userId)
        {
            var content = SetRequestContent(etab);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
            
            var res = await _httpClient.PostAsync(_URL + $"/create/id?id={userId}", content);

            //res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                return new GeneralResponse(true, "Etablissement créer avec succès.");
            }
            else
            {
                return new GeneralResponse(false, "Une erreur est survenue.");
            }
        }

        public async void SetParcSession(int idParc)
        {
            var res = await Get(idParc);

            parcSession = new ParcSession(res.Id, res.Nom); 
        }
    }
}
