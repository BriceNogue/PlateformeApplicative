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
    public class PostesController(PosteService _service) : ControllerBase
    {
        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public ActionResult<List<Poste>> GetAll()
        {
            var postes = _service.GetAll();
            if(postes.Count == 0)
                return NotFound(new List<Poste>());
            return Ok(postes);
        }

        [HttpGet("all/id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public ActionResult<List<Poste>> GetAllByParc(int id)
        {
            var postes = _service.GetAllByParc(id);
            if (postes.Count == 0)
                return NotFound(new List<Poste>());
            return Ok(postes);
        }

        [HttpGet("id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin,User")]
        public ActionResult<Poste> Get(int id)
        {
            var poste = _service.Get(id);
            if (poste is null)
                return NotFound("Poste inexistant.");
            return Ok(poste);
        }

        [HttpPost("one")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin,User")]
        public ActionResult<Poste> GetOne(PosteLoginModele posteLogin)
        {
            var poste = _service.GetOne(posteLogin);
            if (poste is null)
                return NotFound("Poste inexistant.");
            return Ok(poste);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Add(PosteModele poste)
        {
            var result = _service.Add(poste);
            if (result)
                return Ok("Poste cree avec succes.");
            return BadRequest("Impossible de creer le poste.");
        }

        [HttpDelete("delete/id")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok("Poste supprimer avec succes.");
            return BadRequest("Impossible de supprimer le poste.");
        }

        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        public IActionResult Update(PosteModele poste)
        {
            var result = _service.Update(poste);
            if (result)
                return Ok("Poste mis a jour avec succes.");
            return BadRequest("Impossible de mettre a jour le poste.");
        }
    }
}
