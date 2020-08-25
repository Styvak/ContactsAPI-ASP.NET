using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ContactsAPI.Controllers.Requests.Queries;
using ContactsAPI.Controllers.Responses;
using ContactsAPI.Extensions;
using ContactsAPI.Filters;
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
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUriService _uriService;

        public ContactsController(IContactService contactService, IUriService uriService)
        {
            _contactService = contactService;
            _uriService = uriService;
        }

        /// <summary>
        /// Get all the contacts of the user
        /// </summary>
        /// <response code="200">Returns all the contacts of the user</response>
        /// <param name="paginationQuery"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<Contact>), 200)]
        public async Task<IActionResult> GetContacts([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = new PaginationFilter { PageNumber = paginationQuery.PageNumber, PageSize = paginationQuery.PageSize };
            var contacts = await _contactService.GetContactsByUserAsync(HttpContext.GetUserId(), paginationFilter);

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<Contact>(contacts));
            }

            var nextPage = paginationFilter.PageNumber >= 1 ? _uriService.GetContactsUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString() : null;
            var previousPage = paginationFilter.PageNumber - 1 >= 1 ? _uriService.GetContactsUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString() : null;

            var paginationResponse = new PagedResponse<Contact>
            {
                Data = contacts,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = contacts.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
            return Ok(paginationResponse);
        }
    }
}
