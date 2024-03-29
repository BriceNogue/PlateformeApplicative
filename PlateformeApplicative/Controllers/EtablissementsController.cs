using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementsController : ControllerBase
    {
        private readonly EtablissementService _service;

        public EtablissementsController() 
        {
            _service = new EtablissementService();
        }

        [HttpGet("all")]
        public ActionResult<List<Etablissement>> GetAll()
        {
            var result = _service.GetAll();
            if (result.Count == 0)
                return NotFound("Aucun etablissement enregistre.");
            return (result);
        }

        [HttpGet("id")]
        public ActionResult<Etablissement> Get(int id)
        {
            var result = _service.Get(id);
            if (result is null)
                return NotFound("Etablissement inexistant.");
            return Ok(result);
        }

        [HttpPost("create")]
        public ActionResult<Etablissement> Add(EtablissementModele item)
        {
            var result = _service.Add(item);
            if (result)
                return Ok("Etablissement enregistre avec succes.");
            return BadRequest("Impossible d'enregistrer l'etablissement.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok("Etablissement supprime avec succes.");
            return NotFound("L'etablissement n'exite pas.");
        }

        [HttpPut("update")]
        public IActionResult Update(EtablissementModele item)
        {
            var result = _service.Update(item);
            if (result)
                return Ok("Etablissement mis a jour avec succes.");
            return BadRequest("Impossible de mettre a jour l'etablissement.");
        }
    }
}
