﻿using ContactsAPI.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Responses
{
    public class SkillListResponseExample : IExamplesProvider<IEnumerable<Skill>>
    {
        public IEnumerable<Skill> GetExamples()
        {
            return new List<Skill>
            {
                new Skill
                {
                    SkillID = Guid.Parse("13e79370-c541-4507-ba18-54fc60fca9a7"),
                    ContactID = Guid.Parse("13e79370-c541-4507-ba18-54fc60fca9a7"),
                    UserID = "13e79370-c541-4507-ba18-54fc60fca9a7",
                    Name = "Skill name",
                    Level = "Skill level"
                }
            };
        }
    }
}
