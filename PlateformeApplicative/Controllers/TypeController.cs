using Domain.Entities;
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

        [HttpGet("types")]
        public ActionResult<List<TypeE>> GetAll()
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

        [HttpGet("type/id")]
        public ActionResult<TypeE> Get(int id)
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
    }
}
