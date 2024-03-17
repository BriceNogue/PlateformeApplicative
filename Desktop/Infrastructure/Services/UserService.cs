using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Infrastructure.Services
{
    public class UserService
    {
        private readonly HttpClient httpClient;
        private readonly string _URL = "";

        //public static UserModele user;

        public UserService() 
        { 
            httpClient = new HttpClient();
        }


    }
}
