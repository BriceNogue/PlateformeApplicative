using Domain.Entities;
using Infrastructure.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController() 
        {
            _userService = new UserService();
        }

        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            var users = new List<User>();
            users = _userService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("id")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.Get(id);
            if (user is null)
                return NotFound("Utilisateur inexistant.");
            return Ok(user);
        }

        [HttpPost("create")]
        public ActionResult<UserModele> Add(UserModele user) 
        {
            var isAdded = _userService.Add(user);
            if (isAdded)
                return Ok("Utilisateur ajoute avec suces.");
            return BadRequest("impossible d'ajouter l'utilisateur.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _userService.Delete(id);
            if (isDeleted)
                return Ok("Utilisateur supprime avec suces.");
            return NotFound("Utilisateur inexistant.");
        }

        [HttpPut("update")]
        public IActionResult Delele(UserModele user)
        {
            var isUpdated = _userService.Update(user);
            if (isUpdated)
                return Ok("Utilisateur mis a jours avec suces.");
            return BadRequest("Mise a jours impossible.");
        }
    }
}
