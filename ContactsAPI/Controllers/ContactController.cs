using System;
using System.Collections.Generic;
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
            Contact contact = new Contact { Firstname = contactRequest.Firstname, Lastname = contactRequest.Lastname, Fullname = contactRequest.Fullname, Address = contactRequest.Address, Email = contactRequest.Email, Phone = contactRequest.Phone };
            if (contact.Fullname == null) contact.Fullname = $"{contact.Firstname} {contact.Lastname}";
            contact.ContactID = Guid.NewGuid();

            bool created = await _contactService.CreateContactAsync(contact);
            return Ok(contact);
        }
    }
}
