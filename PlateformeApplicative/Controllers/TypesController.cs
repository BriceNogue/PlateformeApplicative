using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly TypeService _typeService;

        public TypesController()
        {
            _typeService = new TypeService();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<TypeE> types = new List<TypeE>();
            types = _typeService.GetAll();
            if (types.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(types);
            }
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            TypeE typeE = new TypeE();
            typeE = _typeService.Get(id);
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
        public ActionResult<TypeModele> Add(TypeModele type)
        {
            var result = _typeService.Add(type);
            if (result)
                return Ok("Type enregistre avec succes.");
            return BadRequest("Impossible d'enregistrer le type.");
        }
    }
}
