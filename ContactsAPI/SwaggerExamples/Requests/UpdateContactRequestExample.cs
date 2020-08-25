using ContactsAPI.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Requests
{
    public class UpdateContactRequestExample : IExamplesProvider<UpdateContactRequest>
    {
        public UpdateContactRequest GetExamples()
        {
            return new UpdateContactRequest
            {
                Firstname = "Another first name",
                Phone = "+33 0 00 00 00 00"
            };
        }
    }
}
