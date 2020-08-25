using ContactsAPI.Controllers.Requests.Queries;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public interface IUriService
    {
        Uri GetContactUri(string contactId);
        Uri GetSkillUri(string skillId);
        Uri GetContactsUri(PaginationQuery pagination = null);
    }
}
