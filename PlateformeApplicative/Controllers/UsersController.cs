using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController() 
        {
            _userService = new UserService();
        }

        [HttpGet("all")]
        public ActionResult<List<Utilisateur>> GetAll()
        {
            var users = new List<Utilisateur>();
            users = _userService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("id")]
        public ActionResult<Utilisateur> Get(int id)
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
                return Ok("Utilisateur ajoute avec succes.");
            return BadRequest("impossible d'ajouter l'utilisateur.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _userService.Delete(id);
            if (isDeleted)
                return Ok("Utilisateur supprime avec succes.");
            return NotFound("Utilisateur inexistant.");
        }

        [HttpPut("update")]
        public IActionResult Update(UserModele user)
        {
            var isUpdated = _userService.Update(user);
            if (isUpdated)
                return Ok("Utilisateur mis a jour avec succes.");
            return BadRequest("Mise a jour impossible.");
        }
    }
}
