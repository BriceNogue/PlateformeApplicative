using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController(TypeService _typeService) : ControllerBase
    {
        [HttpGet("all")]
        public ActionResult<List<TypeModele>> GetAll()
        {
            var types = _typeService.GetAll();
            if (types.Count == 0)
            {
                return new List<TypeModele>();
            }
            else
            {
                return Ok(types);
            }
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var typeE = _typeService.Get(id);
            if (typeE is null)
            {
                return NotFound("Type not found.");
            }
            else
            {
                return Ok(typeE);
            }
        }

        [HttpPost("create")]
        public async Task <ActionResult<TypeModele>> Add(TypeModele type)
        {
            var result = await _typeService.Add(type);
            if (result.Flag)
                return Ok("Type enregistre avec succes.");

            else
            {
                return BadRequest("Impossible d'enregistrer le type.");
            }
        }
    }
}
