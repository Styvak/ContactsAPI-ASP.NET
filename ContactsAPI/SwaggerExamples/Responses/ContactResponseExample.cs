using ContactsAPI.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Responses
{
    public class ContactResponseExample : IExamplesProvider<Contact>
    {
        public Contact GetExamples()
        {
            return new Contact
            {
                ContactID = Guid.Parse("13e79370-c541-4507-ba18-54fc60fca9a7"),
                Firstname = "Firstname",
                Lastname = "Lastname",
                Address = "Contact's address",
                Email = "email@example.com",
                Phone = "+33 0 00 00 00 00",
                UserID = "13e79370-c541-4507-ba18-54fc60fca9a7",
                Skills = new List<Skill> {
                    new Skill
                    {
                        SkillID = Guid.Parse("a7e79370-c541-4507-ba18-54fc60fca9a7"),
                        Name = "Skill name",
                        Level = "Skill level",
                        UserID = "13e79370-c541-4507-ba18-54fc60fca9a7"
                    }
                }
            };
        }
    }
}
