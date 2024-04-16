using Newtonsoft.Json;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class TypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/types";

        public TypeService()
        {
            _httpClient = new HttpClient();
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
