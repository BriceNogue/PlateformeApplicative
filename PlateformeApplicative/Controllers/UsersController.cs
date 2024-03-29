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

        [HttpGet("all")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public ActionResult<List<Utilisateur>> GetAll()
        {
            var users = new List<Utilisateur>();
            users = _userService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("id")]
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        public ActionResult<Utilisateur> Get(int id)
        {
            var user = _userService.Get(id);
            if (user is null)
                return NotFound("Utilisateur inexistant.");
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add(UserModele user) 
        {
            if (user is null)
            {
                return BadRequest(new GeneralResponse(false, "Aucune donnée."));
            }
            else
            {
                var res = await _userService.Add(user);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModele loginM)
        {
            var res = await _userService.Login(loginM);

            return Ok(res);
        }
    }
}
