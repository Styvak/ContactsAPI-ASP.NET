using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactRequest contactRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Contact contact = new Contact { Firstname = contactRequest.Firstname, Lastname = contactRequest.Lastname, Fullname = contactRequest.Fullname, Address = contactRequest.Address, Email = contactRequest.Email, Phone = contactRequest.Phone };
            if (contact.Fullname == null) contact.Fullname = $"{contact.Firstname} {contact.Lastname}";
            contact.ContactID = Guid.NewGuid();

            bool created = await _contactService.CreateContactAsync(contact);

            var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/api/contact/{contact.ContactID}";
            return Created(locationUri, contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _contactService.DeleteContactByIdAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContactRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            if (request.Firstname != null) contact.Firstname = request.Firstname;
            if (request.Lastname != null) contact.Lastname = request.Lastname;
            if (request.Fullname != null) contact.Fullname = request.Fullname;
            else if (request.Firstname != null || request.Lastname != null) contact.Fullname = $"{contact.Firstname} {contact.Lastname}";
            if (request.Phone != null) contact.Phone = request.Phone;
            if (request.Email != null) contact.Email = request.Email;
            if (request.Address != null) contact.Address = request.Address;

            var updated = await _contactService.UpdateContact(contact);
            if (updated)
                return Ok(contact);
            return NotFound();
        }
    }
}
