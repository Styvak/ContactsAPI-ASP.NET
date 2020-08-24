using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
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
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillRequest skillRequest)
        {
            Skill skill = new Skill { Name = skillRequest.Name, Level = skillRequest.Level, ContactID = skillRequest.ContactID };
            skill.SkillID = Guid.NewGuid();

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
            var deleted = await _skillService.DeleteSkillByIdAsync(id);
            //TODO -> Remove skill from contact
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}
