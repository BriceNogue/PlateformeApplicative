
using Newtonsoft.Json;
using Shareds.Modeles;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Mobile.Services
{
    public class TypeService
    {
        private readonly HttpClient _httpClient = default!;

        // Definit l'url de base en local en fonction de la plateforme car l'émultateur n'a pas directement accès à notre ordi
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7281" : "https://localhost:7281";

        private readonly string _URL = $"{BaseAddress}/api/types";

        public TypeService()
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _httpClient  = new HttpClient();
#endif

        }

        public async Task<List<TypeModele>> GetAll()
        {
            var res = await _httpClient.GetAsync(_URL + "/all");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<TypeModele>>(jsonString);
                return data!;
            }
            else
            {
                return new List<TypeModele>();
            }
        }

        public async Task<TypeModele> GetById(int id)
        {
            var res = await _httpClient.GetAsync(_URL + $"/id?id={id}");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TypeModele>(jsonString);
                return data!;
            }
            else
            {
                return new TypeModele();
            }
        }
    }
}
