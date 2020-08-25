using ContactsAPI.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Requests
{
    public class CreateSkillRequestExample : IExamplesProvider<SkillRequest>
    {
        public SkillRequest GetExamples()
        {
            return new SkillRequest
            {
                ContactID = Guid.Parse("13e79370-c541-4507-ba18-54fc60fca9a7"),
                Name = "Skill name",
                Level = "Skill level"
            };
        }
    }
}
