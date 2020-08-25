using ContactsAPI.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Requests
{
    public class CreateContactRequestExample : IExamplesProvider<ContactRequest>
    {
        public ContactRequest GetExamples()
        {
            return new ContactRequest
            {
                Firstname = "Firstname",
                Lastname = "Lastname",
                Address = "Contact's address",
                Email = "email@example.com",
                Phone = "+33 0 00 00 00 00"
            };
        }
    }
}
