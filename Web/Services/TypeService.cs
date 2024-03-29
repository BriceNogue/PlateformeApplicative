using Shareds.Modeles;
using System.Text.Json;

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
            var res = await _httpClient.GetAsync(_URL + "/all");

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<TypeModele>>(jsonString);
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
                var data = JsonSerializer.Deserialize<TypeModele>(jsonString);
                return data!;
            }
            else
            {
                return new TypeModele();
            }
        }
    }
}
