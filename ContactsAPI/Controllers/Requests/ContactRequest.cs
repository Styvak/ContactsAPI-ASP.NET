using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers.Requests
{
    public class ContactRequest
    {
        [Required] public string Firstname { get; set; }
        [Required] public string Lastname { get; set; }
        public string Fullname { get; set; }
        [Required] public string Address { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [Phone] public string Phone { get; set; }
    }
}
