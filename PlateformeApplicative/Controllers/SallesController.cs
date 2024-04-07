using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController(SalleService _service) : ControllerBase
    {
        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public ActionResult<List<Salle>> GetAll()
        {
            var result = _service.GetAll();
            if (result is null)
                return NoContent();
            return result;
        }

        [HttpGet("id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin,User")]
        public ActionResult<Salle> Get(int id)
        {
            var result = _service.Get(id);
            if (result is null)
                return NoContent();
            return result;
        }

        [HttpGet("all/id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public ActionResult<List<Salle>> GetAllByParc(int id)
        {
            var result = _service.GetAllByParc(id);
            if (result is null)
                return NoContent();
            return result;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Add(SalleModele salle)
        {
            var result = _service.Add(salle);
            if (result)
                return Ok("Salle Enregistre avec succes.");
            return BadRequest("Impossible d'enregistrer la salle.");
        }

        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok("Salle supprime avec succes.");
            return BadRequest("Impossible de supprimer la salle.");
        }

        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Update(SalleModele salle)
        {
            var result = _service.Update(salle);
            if (result)
                return Ok("Salle mise a jour avec succes.");
            return BadRequest("Impossible de mettre a jour la salle.");
        }
    }
}
