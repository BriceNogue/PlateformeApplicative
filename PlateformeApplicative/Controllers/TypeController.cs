using Domain.Entities;
using Infrastructure.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly TypeService _typeService;

        public TypeController()
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

        [HttpPost("add")]
        public ActionResult<TypeModele> Add(TypeModele type)
        {
            return Ok(_typeService.Add(type));
        }
    }
}
