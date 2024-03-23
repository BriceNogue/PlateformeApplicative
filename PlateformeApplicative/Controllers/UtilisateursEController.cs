using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Shareds.Modeles;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursEController : Controller
    {
        private readonly UEService _ueService;

        public UtilisateursEController(UEService ueService)
        {
            _ueService = ueService;
        }

        [HttpGet("all")]
        public ActionResult<List<UtilisateurEtablissement>> GetAll()
        {
            var users = new List<UtilisateurEtablissement>();
            users = _ueService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("all/id")]
        public ActionResult<List<UtilisateurEtablissement>> GetAllByUserId(int id)
        {
            var res = new List<UtilisateurEtablissement>();
            res = _ueService.GetAll();
            if (res.Count == 0)
                return NoContent();
            return Ok(res.Where(eu => eu.IdUtilisateur == id));
        }

        [HttpGet("id")]
        public ActionResult<UtilisateurEtablissement> Get(int id)
        {
            var user = _ueService.Get(id);
            if (user is null)
                return NotFound("Utilisateur inexistant.");
            return Ok(user);
        }

        [HttpPost("create")]
        public ActionResult<UEModele> Add(UEModele user)
        {
            var isAdded = _ueService.Add(user);
            if (isAdded)
                return Ok("Utilisateur ajoute avec succes.");
            return BadRequest("impossible d'ajouter l'utilisateur.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _ueService.Delete(id);
            if (isDeleted)
                return Ok("Utilisateur supprime avec succes.");
            return NotFound("Utilisateur inexistant.");
        }

        [HttpPut("update")]
        public IActionResult Update(UEModele user)
        {
            var isUpdated = _ueService.Update(user);
            if (isUpdated)
                return Ok("Utilisateur mis a jour avec succes.");
            return BadRequest("Mise a jour impossible.");
        }
    }
}
