using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers.Requests
{
    public class UpdateContactRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        [EmailAddress] public string Email { get; set; }
        [Phone] public string Phone { get; set; }
    }
}
