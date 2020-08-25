using ContactsAPI.Controllers.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetContactsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null) return uri;
            var modifyUri = QueryHelpers.AddQueryString(_baseUri, "pagenumber", pagination.PageNumber.ToString());
            modifyUri = QueryHelpers.AddQueryString(modifyUri, "pagesize", pagination.PageSize.ToString());
            return new Uri(modifyUri);
        }

        public Uri GetContactUri(string contactId)
        {
            //var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/api/contact/{contact.ContactID}";
            return new Uri(_baseUri + $"/api/contact/{contactId}");
        }

        public Uri GetSkillUri(string skillId)
        {
            return new Uri(_baseUri + $"/api/skill/{skillId}");
        }
    }
}
