using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class Skill
    {
        [Key] public Guid SkillID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Level { get; set; }
        [Required] public Guid ContactID { get; set; }
        [ForeignKey(nameof(ContactID))] [JsonIgnore] [Required] public Contact Contact { get; set; }
        [Required] public string UserID { get; set; }
        [ForeignKey(nameof(UserID))] [JsonIgnore] public IdentityUser User { get; set; }
    }
}
