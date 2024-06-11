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

        public async Task<List<SalleModele>> GetAll(string userToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                
                List<SalleModele>? salles = await _httpClient.GetFromJsonAsync<List<SalleModele>>(_URL + "/all");

                if (salles != null)
                {
                    return salles;
                }
                else
                {
                    return new List<SalleModele>();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<SalleModele>> GetAllByParc(int parcId, string userToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

                var res = await _httpClient.GetAsync(_URL + $"/all/id?id={parcId}");

                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<SalleModele>>(jsonString);

                    return data!;
                }
                else
                {
                    return new List<SalleModele>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<SalleModele> GetById(int id, string userToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

                var res = await _httpClient.GetFromJsonAsync<SalleModele>(_URL + $"/id?id={id}");

                if (res != null)
                {
                    return res;
                }
                else
                {
                    return null!;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private HttpContent SetRequestContent(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }

        public async Task<GeneralResponse> Create(SalleModele salle, string userToken)
        {
            try
            {
                var content = SetRequestContent(salle);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

                HttpResponseMessage res = await _httpClient.PostAsync(_URL + "/create", content);

                if (res.IsSuccessStatusCode)
                {
                    return new GeneralResponse(true, "Salle créer avec succès.");
                }
                else
                {

                    return new GeneralResponse(false, "Une erreur est survenue.");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
