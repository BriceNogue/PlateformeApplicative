using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController(SalleService _service) : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<List<Salle>> GetAll()
        {
            var result = _service.GetAll();
            if (result is null)
                return NoContent();
            return result;
        }

        [HttpGet("id")]
        public ActionResult<Salle> Get(int id)
        {
            var result = _service.Get(id);
            if (result is null)
                return NoContent();
            return result;
        }

        [HttpPost("create")]
        public IActionResult Add(SalleModele salle)
        {
            var result = _service.Add(salle);
            if (result)
                return Ok("Salle Enregistre avec succes.");
            return BadRequest("Impossible d'enregistrer la salle.");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok("Salle supprime avec succes.");
            return BadRequest("Impossible de supprimer la salle.");
        }

        [HttpPut("update")]
        public IActionResult Update(SalleModele salle)
        {
            var result = _service.Update(salle);
            if (result)
                return Ok("Salle mise a jour avec succes.");
            return BadRequest("Impossible de mettre a jour la salle.");
        }
    }
}
