using Shareds.Modeles;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Mobile.Services
{
    public class RoomService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/salles";

        public RoomService()
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _httpClient  = new HttpClient();
#endif

        }

        public async Task<List<SalleModele>> GetAllByParc(int parcId, string userToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

                List<SalleModele> salles = await _httpClient.GetFromJsonAsync<List<SalleModele>>(_URL + $"/all/id?id={parcId}");

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
                throw new Exception(ex.Message);
            }

        }
    }
}
