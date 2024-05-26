
using Mobile.Domain.Repositories;
using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;

namespace Mobile.Services
{
    public class ParkService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/etablissements";

        ParcSessionRepository parkSR;

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
    }
}
