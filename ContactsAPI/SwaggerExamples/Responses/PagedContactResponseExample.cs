using ContactsAPI.Controllers.Responses;
using ContactsAPI.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Responses
{
    public class PagedContactResponseExample : IExamplesProvider<PagedResponse<Contact>>
    {
        public PagedResponse<Contact> GetExamples()
        {
            return new PagedResponse<Contact>
            {
                Data = new[] {
                    new Contact {
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
                    }
                },
                PageNumber = 1,
                PageSize = 1,
                NextPage = "http://localhost:5000/api/contacts?pagenumber=1&pagesize=1",
                PreviousPage = null
            };
        }
    }
}
