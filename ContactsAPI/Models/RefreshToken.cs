using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class RefreshToken
    {
        [Key] public string Token { get; set; }
        [Required] public string JwtID { get; set; }
        [Required] public DateTime CreationDate { get; set; }
        [Required] public DateTime ExpiryDate { get; set; }
        [Required] public bool Invalidated { get; set; }
        [Required] public bool Used { get; set; }
        [Required] public string UserID { get; set; }
        [ForeignKey(nameof(UserID))] public IdentityUser User { get; set; }
    }
}
