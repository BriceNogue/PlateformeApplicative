using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;
using static Shareds.Modeles.ResponsesModels;
using System.Text;
using Microsoft.JSInterop;

namespace Web.Services
{
    public class ParcService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/etablissements";

        //public static ParcSession? parcSession;
        private IJSRuntime JS;

        public ParcService(IJSRuntime js)
        {
            _httpClient = new HttpClient();
            JS = js;
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

        public async Task<List<EtablissementModele>> GetAllByUser(int id, string userToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/all/id?id={id}");

            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task <EtablissementModele> Get(int id, string userToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/id?id={id}");

            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EtablissementModele> GetByName(string name, string userToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/name?name={name}");

            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var res = await _httpClient.SendAsync(request);

                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EtablissementModele>(jsonString);
                    return data!;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Pour addapter le contenu de la requete
        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<GeneralResponse> Create(EtablissementModele etab, int userId, string userToken)
        {
            var content = SetRequestContent(etab);

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

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
            }catch (Exception ex) { throw new Exception(ex.Message) ; }
        }

        #region /// Parc Session

        public async void SetParcSession(EtablissementModele etab)
        {
            ParcSession parcSession = new ParcSession(etab.Id, etab.Nom);
            string jsonParc = JsonConvert.SerializeObject(parcSession);
            await JS.InvokeVoidAsync("localStorage.setItem", "ParcSession", jsonParc);
        }

        public async Task<ParcSession> GetParcSession()
        {
            try
            {
                var res = await JS.InvokeAsync<string>("localStorage.getItem", "ParcSession");
                if (res != null)
                {
                    ParcSession parcS = JsonConvert.DeserializeObject<ParcSession>(res)!;
                    return parcS;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveParcSession()
        {
            try
            {
                await JS.InvokeVoidAsync("localStorage.removeItem", "ParcSession");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
