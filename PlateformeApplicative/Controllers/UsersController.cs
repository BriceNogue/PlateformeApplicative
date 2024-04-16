using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Shareds.Modeles.ResponsesModels;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModele loginM)
        {
            var res = await _userService.Login(loginM);

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModele user)
        {
            if (user is null)
            {
                return BadRequest(new GeneralResponse(false, "Aucune donnée."));
            }
            else
            {
                var res = await _userService.Register(user);
                if (res.Flag)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
        }

        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult<List<UserModele>>> GetAll()
        {
            var users = new List<UserModele>();
            users = await _userService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin,User")]
        public ActionResult<UserModele> GetBy(int id)
        {
            var user = _userService.Get(id);
            if (user is null)
                return NotFound("Utilisateur inexistant.");
            return Ok(user);
        }

        [HttpGet("users_parc/id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public ActionResult<List<UserModele>> GetAllByParc(int id)
        {
            var usersParc = new List<UserModele>();
            usersParc = _userService.GetAllByParc(id);
            if (usersParc.Count == 0)
                return NoContent();
            return Ok(usersParc);
        }

        // Pour ajouter un utilisateur a un par grace a l'id du parc
        [HttpPost("add/id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Add(UserModele user, int id) 
        {
            if (user is null)
            {
                return BadRequest(new GeneralResponse(false, "Aucune donnée."));
            }
            else if (user.IdType == 0)
            {
                return BadRequest(new GeneralResponse(false, "Veillez renseigner le role de l'utilisateur."));
            }
            else
            {
                var res = await _userService.Add(user, id);
                if (res.Flag)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
        }

        [HttpDelete("delete/id")]
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _userService.Delete(id);
            if (isDeleted)
                return Ok("Utilisateur supprime avec succes.");
            return NotFound("Utilisateur inexistant.");
        }

        [HttpPut("update")]
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        public IActionResult Update(UserModele user)
        {
            var isUpdated = _userService.Update(user);
            if (isUpdated)
                return Ok("Utilisateur mis a jour avec succes.");
            return BadRequest("Mise a jour impossible.");
        }
    }
}
