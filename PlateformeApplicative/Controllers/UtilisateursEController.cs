﻿using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Shareds.Modeles;

namespace PlateformeApplicative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursEController : Controller
    {
        private readonly UEService _ueService;

        public UtilisateursEController()
        {
            _ueService = new UEService();
        }

        [HttpGet("all")]
        public ActionResult<List<UtilisateurEtablissement>> GetAll()
        {
            var users = new List<UtilisateurEtablissement>();
            users = _ueService.GetAll();
            if (users.Count == 0)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("id")]
        public ActionResult<UtilisateurEtablissement> Get(int id)
        {
            var user = _ueService.Get(id);
            if (user is null)
                return NotFound("Utilisateur inexistant.");
            return Ok(user);
        }

        [HttpPost("create")]
        public ActionResult<UEModele> Add(UEModele user)
        {
            var isAdded = _ueService.Add(user);
            if (isAdded)
                return Ok("Utilisateur ajoute avec succes.");
            return BadRequest("impossible d'ajouter l'utilisateur.");
        }

        [HttpDelete("delete/id")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _ueService.Delete(id);
            if (isDeleted)
                return Ok("Utilisateur supprime avec succes.");
            return NotFound("Utilisateur inexistant.");
        }

        [HttpPut("update")]
        public IActionResult Delele(UEModele user)
        {
            var isUpdated = _ueService.Update(user);
            if (isUpdated)
                return Ok("Utilisateur mis a jour avec succes.");
            return BadRequest("Mise a jour impossible.");
        }
    }
}
