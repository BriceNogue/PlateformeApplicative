using Shareds.Modeles;

namespace Web.Services
{
    public class TypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/types";

        public TypeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task <List<TypeModele>> GetAll() {
            List<TypeModele>? res = await _httpClient.GetFromJsonAsync<List<TypeModele>>(_URL + "/all");

            if (res is not null)
            {
                return res;
            }
            else
            {
                return new List<TypeModele>();
            }
        }

        public async Task<TypeModele> GetById(int id)
        {
            TypeModele? res = await _httpClient.GetFromJsonAsync<TypeModele>(_URL + $"/id?id={id}");

            if (res is not null)
            {
                return res;
            }
            else
            {
                return new TypeModele();
            }
        }
    }
}
