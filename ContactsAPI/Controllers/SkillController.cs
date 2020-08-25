using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactsAPI.Controllers.Requests;
using ContactsAPI.Controllers.Responses;
using ContactsAPI.Extensions;
using ContactsAPI.Models;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IContactService _contactService;
        private readonly IUriService _uriService;

        public SkillController(ISkillService skillService, IContactService contactService, IUriService uriService)
        {
            _skillService = skillService;
            _contactService = contactService;
            _uriService = uriService;
        }

        /// <summary>
        /// Get a specific skill
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Get a specific skill</response>
        /// <response code="404">Skill not found</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Skill), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userOwnsSkill = await _skillService.UserOwnsSkillAsync(id, HttpContext.GetUserId());

            if (!userOwnsSkill) return NotFound();

            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(new Response<Skill>(skill));
        }

        /// <summary>
        /// Create a skill
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <response code="201">Skill created</response>
        /// <response code="404">Bad request</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Skill), 201)]
        public async Task<IActionResult> Create([FromBody] SkillRequest skillRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Skill skill = new Skill { Name = skillRequest.Name, Level = skillRequest.Level, ContactID = skillRequest.ContactID };
            skill.SkillID = Guid.NewGuid();
            skill.UserID = HttpContext.GetUserId();

            skill.Contact = await _contactService.GetContactByIdAsync(skill.ContactID);

            if (skill.Contact == null)
            {
                ModelState.AddModelError("ContactID", $"Contact {skill.ContactID} not found.");
                return BadRequest(ModelState);
            }

            bool created = await _skillService.CreateSkillAsync(skill);

            var locationUri = _uriService.GetSkillUri(skill.SkillID.ToString());
            return Created(locationUri, new Response<Skill>(skill));
        }

        /// <summary>
        /// Delete a skill
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Skill deleted</response>
        /// <response code="404">Skill not found</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userOwnsSkill = await _skillService.UserOwnsSkillAsync(id, HttpContext.GetUserId());

            if (!userOwnsSkill) return NotFound();

            var deleted = await _skillService.DeleteSkillByIdAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }

        /// <summary>
        /// Update a skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <response code="200">Skill updated</response>
        /// <response code="404">Skill not found</response>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Skill), 200)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSkillRequest request)
        {
            var userOwnsSkill = await _skillService.UserOwnsSkillAsync(id, HttpContext.GetUserId());

            if (!userOwnsSkill) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null) return NotFound();
            if (request.ContactID != null)
            {
                skill.ContactID = (Guid)request.ContactID;
                skill.Contact = await _contactService.GetContactByIdAsync(skill.ContactID);
                if (skill.Contact == null)
                {
                    ModelState.AddModelError("ContactID", $"Contact {skill.ContactID} not found.");
                    return BadRequest(ModelState);
                }
            }
            if (request.Name != null) skill.Name = request.Name;
            if (request.Level != null) skill.Level = request.Level;

            var updated = await _skillService.UpdateSkill(skill);
            if (updated)
                return Ok(new Response<Skill>(skill));
            return NotFound();
        }
    }
}
