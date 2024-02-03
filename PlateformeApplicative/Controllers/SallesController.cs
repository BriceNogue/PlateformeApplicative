using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly SalleService _service;

        public SallesController() 
        {
            _service = new SalleService();
        }

        [HttpGet("all")]
        public ActionResult<List<Salle>> GetAll()
        {
            var result = _service.GetAll();
            if (result is null)
                return NoContent();
            return result;
        }
    }
}
