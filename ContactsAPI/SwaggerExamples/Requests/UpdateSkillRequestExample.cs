using ContactsAPI.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Requests
{
    public class UpdateSkillRequestExample : IExamplesProvider<UpdateSkillRequest>
    {
        public UpdateSkillRequest GetExamples()
        {
            return new UpdateSkillRequest
            {
                Level = "A better level"
            };
        }
    }
}
