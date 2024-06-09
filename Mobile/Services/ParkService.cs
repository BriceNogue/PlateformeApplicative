
using Mobile.Domain.Entities;
using Mobile.Domain.Repositories;
using Newtonsoft.Json;
using Shareds.Modeles;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using ParcSession = Mobile.Domain.Entities.ParcSession;

namespace Mobile.Services
{
    public class ParkService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/etablissements";

        ParcSessionRepository parkSR;

        //public static ParcSession parcSession = null;

        public ParkService()
        {
            parkSR = new ParcSessionRepository();
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _httpClient  = new HttpClient();
#endif

        }

        public void SaveParkPreferences(ParcSession parkSession)
        {
            string jsonParkSession = JsonConvert.SerializeObject(parkSession);
            Preferences.Default.Set("park_session", jsonParkSession);
        }

        public ParcSession GetParkPreferences()
        {
            try
            {
                ParcSession parkSession = null;
                bool haskey = Preferences.Default.ContainsKey("park_session");

                if (haskey)
                {
                    var jsonParkSession = Preferences.Default.Get("park_session", "{}");

                    parkSession = new ParcSession();
                    parkSession = JsonConvert.DeserializeObject<ParcSession>(jsonParkSession);
                }

                return parkSession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteParkPreferences()
        {
            try
            {
                bool haskey = Preferences.Default.ContainsKey("park_session");

                if (haskey)
                {
                    Preferences.Default.Remove("park_session");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task<EtablissementModele> GetById(int id, string userToken)
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
    }
}
