using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class Contact
    {
        [Key] public Guid ContactID { get; set; }
        [Required] public string Firstname { get; set; }
        [Required] public string Lastname { get; set; }
        public string Fullname { get; set; }
        [Required] public string Address { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [Phone] public string Phone { get; set; }
        public ICollection<Skill> Skills { get; set; }
        [Required] public string UserID { get; set; }
        [ForeignKey(nameof(UserID))] public IdentityUser User { get; set; }
    }
}
