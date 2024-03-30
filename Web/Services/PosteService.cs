﻿using Shareds.Modeles;
using System.Text.Json;

namespace Web.Services
{
    public class PosteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "https://localhost:7281/api/postes";

        public PosteService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<PosteModele>> GetAll()
        {
            var res = await _httpClient.GetAsync(_URL + "/all");
            
            if (res.IsSuccessStatusCode) 
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<PosteModele>>(jsonString);

                return data!;
            }
            else
            {
                return new List<PosteModele>();
            }

        }

        public async Task<PosteModele> GetById(int id)
        {
            PosteModele? poste = await _httpClient.GetFromJsonAsync<PosteModele>(_URL + $"/id?id={id}");

            if (poste != null)
            {
                return poste;
            }
            else
            {
                return new PosteModele();
            }

        }
    }
}
