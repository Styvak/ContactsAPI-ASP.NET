using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /// <summary>
        /// Get list of skills
        /// </summary>
        /// <response code="200">Get skills</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Skill>), 200)]
        public async Task<IActionResult> GetSkills()
        {
            return Ok(await _skillService.GetSkillsByUserAsync(HttpContext.GetUserId()));
        }

        /// <summary>
        /// Get list of skills for a specific contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <response code="200">Get skills for a contact</response>
        /// <returns></returns>
        [HttpGet("contact/{contactId}")]
        [ProducesResponseType(typeof(IEnumerable<Skill>), 200)]
        public async Task<IActionResult> GetSkillsByContact(Guid contactId)
        {
            return Ok(new Response<IEnumerable<Skill>>(await _skillService.GetSkillsByContactIdAsync(contactId)));
        }
    }
}
