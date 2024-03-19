using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shareds.Modeles.ResponsesModels;

namespace Shareds.Interfaces
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserModele userDTO);
        Task<LoginResponse> LoginAccount(UserLoginModele loginDTO);
    }
}
