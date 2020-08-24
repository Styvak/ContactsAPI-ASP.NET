using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class SkillRequest
    {
        [Required] public Guid ContactID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Level { get; set; }
    }
}
