using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class SalleService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/salles";

        public SalleService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<SalleModele>> GetAllByParc(int idParc)
        {
            if (UserService.userToken is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
            }

            List<SalleModele>? salles = await _httpClient.GetFromJsonAsync<List<SalleModele>>(_URL + $"/all/id?id={idParc}");

            if (salles != null)
            {
                return salles;
            }
            else
            {
                return new List<SalleModele>();
            }

        }

        public async Task<SalleModele> GetById(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);

            var res = await _httpClient.GetFromJsonAsync<SalleModele>(_URL + $"/id?id={id}");

            if (res != null)
            {
                return res;
            }
            else
            {
                return null!;
            }

        }
    }
}
