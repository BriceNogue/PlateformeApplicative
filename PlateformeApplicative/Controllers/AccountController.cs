using Microsoft.AspNetCore.Mvc;
using Shareds.Interfaces;
using Shareds.Modeles;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : Controller
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModele userDTO)
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModele login)
        {
            var response = await userAccount.LoginAccount(login);
            return Ok(response);
        }
    }
}
