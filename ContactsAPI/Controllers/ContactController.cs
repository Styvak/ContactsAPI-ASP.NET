using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactsAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUriService _uriService;

        public ContactController(IContactService contactService, IUriService uriService)
        {
            _contactService = contactService;
            _uriService = uriService;
        }

        /// <summary>
        /// Get a contact
        /// </summary>
        /// <response code="200">Returns the contact</response>
        /// <response code="404">Contact not found</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Contact), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userOwnsContact = await _contactService.UserOwnsContactAsync(id, HttpContext.GetUserId());

            if (!userOwnsContact) return NotFound();
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(new Response<Contact>(contact));
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <response code="201">Returns the contact</response>
        /// <response code="400">Impossible to create the contact due to validation error</response>
        /// <param name="contactRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Contact), 201)]
        [ProducesResponseType(typeof(ModelStateDictionary), 400)]
        public async Task<IActionResult> Create([FromBody] ContactRequest contactRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Contact contact = new Contact { Firstname = contactRequest.Firstname, Lastname = contactRequest.Lastname, Fullname = contactRequest.Fullname, Address = contactRequest.Address, Email = contactRequest.Email, Phone = contactRequest.Phone };
            if (contact.Fullname == null) contact.Fullname = $"{contact.Firstname} {contact.Lastname}";
            contact.ContactID = Guid.NewGuid();
            contact.UserID = HttpContext.GetUserId();

            bool created = await _contactService.CreateContactAsync(contact);

            var locationUri = _uriService.GetContactUri(contact.ContactID.ToString());

            return Created(locationUri, new Response<Contact>(contact));
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Contact deleted</response>
        /// <response code="404">Contact not found</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userOwnsContact = await _contactService.UserOwnsContactAsync(id, HttpContext.GetUserId());

            if (!userOwnsContact) return NotFound();
            var deleted = await _contactService.DeleteContactByIdAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <response code="200">Contact updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Contact not found</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Contact), 200)]
        //TODO-> Add bad request response
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContactRequest request)
        {
            var userOwnsContact = await _contactService.UserOwnsContactAsync(id, HttpContext.GetUserId());

            if (!userOwnsContact) return NotFound();

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
                return Ok(new Response<Contact>(contact));
            return NotFound();
        }
    }
}
