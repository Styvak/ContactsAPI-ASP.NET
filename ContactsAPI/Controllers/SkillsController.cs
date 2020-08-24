using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactsAPI.Extensions;
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
            return Ok(await _skillService.GetSkillsByUserAsync(HttpContext.GetUserId()));
        }

        [HttpGet("contact/{contactId}")]
        public async Task<IActionResult> GetSkillsByContact(Guid contactId)
        {
            return Ok(await _skillService.GetSkillsByContactIdAsync(contactId));
        }
    }
}
