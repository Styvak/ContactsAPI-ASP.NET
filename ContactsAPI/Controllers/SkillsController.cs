using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            return Ok(await _skillService.GetSkillsAsync());
        }

        [HttpGet("contact/{contactId}")]
        public async Task<IActionResult> GetSkillsByContact(Guid contactId)
        {
            return Ok(await _skillService.GetSkillsByContactIdAsync(contactId));
        }
    }
}
