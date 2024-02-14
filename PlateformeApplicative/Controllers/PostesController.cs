using Domain.Entities;
using Shared.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostesController : ControllerBase
    {
        private readonly PosteService _service;

        public PostesController()
        {
            _service = new PosteService();
        }

        [HttpGet("all")]
        public ActionResult<List<Poste>> GetAll()
        {
            var postes = _service.GetAll();
            if(postes.Count == 0)
                return NoContent();
            return Ok(postes);
        }

        [HttpGet("id")]
        public ActionResult<Poste> Get(int id)
        {
            var poste = _service.Get(id);
            if (poste is null)
                return NotFound("Poste inexistant.");
            return Ok(poste);
        }

        [HttpPost("one")]
        public ActionResult<Poste> GetOne(PosteLoginModele posteLogin)
        {
            var poste = _service.GetOne(posteLogin);
            if (poste is null)
                return NotFound("Poste inexistant.");
            return Ok(poste);
        }

        [HttpPost("create")]
        public IActionResult Add(PosteModele poste)
        {
            var result = _service.Add(poste);
            if (result)
                return Ok("Poste cree avec succes.");
            return BadRequest("Impossible de creer le poste.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok("Poste supprimer avec succes.");
            return BadRequest("Impossible de supprimer le poste.");
        }

        [HttpPut("update")]
        public IActionResult Update(PosteModele poste)
        {
            var result = _service.Update(poste);
            if (result)
                return Ok("Poste mis a jour avec succes.");
            return BadRequest("Impossible de mettre a jour le poste.");
        }
    }
}
