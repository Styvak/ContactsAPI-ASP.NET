using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers.Requests
{
    public class UpdateSkillRequest
    {
        public Guid? ContactID { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
    }
}
