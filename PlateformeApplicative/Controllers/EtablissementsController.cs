﻿using Domain.Entities;
using Shareds.Modeles;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementsController(EtablissementService _service) : ControllerBase
    {
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

        [HttpGet("all/id")]
        public ActionResult<List<Etablissement>> GetAllByUser(int id)
        {
            var result = _service.GetAllByUser(id);
            if (result.Count == 0)
                return NotFound("Aucun etablissement enregistre.");
            return Ok(result);
        }

        [HttpPost("create/id")]
        public ActionResult<Etablissement> Add(EtablissementModele etab, int userId)
        {
            var result = _service.Add(etab, userId);
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
