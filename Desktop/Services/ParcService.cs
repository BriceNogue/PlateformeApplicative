using Newtonsoft.Json;
using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class ParcService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/etablissements";

        public static ParcSession? parcSession;

        public ParcService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<EtablissementModele>> GetAllByUser(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/all/id?id={id}");

            if (UserService.userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
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
            else
            {
                return new List<EtablissementModele>();
            }
        }

        public async Task<EtablissementModele> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _URL + $"/id?id={id}");

            if (UserService.userToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserService.userToken);
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
            else
            {
                return null!;
            }
        }

        public async Task<bool> SetParcSession(int idParc)
        {
            var res = await Get(idParc);

            if (res is not null)
            {
                parcSession = new ParcSession(res.Id, res.Nom);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
