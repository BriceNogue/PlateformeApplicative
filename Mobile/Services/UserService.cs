using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient = default!;
        private readonly string _URL = "https://localhost:7281/api/users";

        public UserService() 
        {
            _httpClient = new HttpClient();
        }

        
    }
}
