using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IContactService _contactService;

        public SkillController(ISkillService skillService, IContactService contactService)
        {
            _skillService = skillService;
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userOwnsSkill = await _skillService.UserOwnsSkillAsync(id, HttpContext.GetUserId());

            if (!userOwnsSkill) return NotFound();

            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpPost]
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

            var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/api/skill/{skill.SkillID}";
            return Created(locationUri, skill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userOwnsSkill = await _skillService.UserOwnsSkillAsync(id, HttpContext.GetUserId());

            if (!userOwnsSkill) return NotFound();

            var deleted = await _skillService.DeleteSkillByIdAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }

        [HttpPut("{id}")]
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
                return Ok(skill);
            return NotFound();
        }
    }
}
