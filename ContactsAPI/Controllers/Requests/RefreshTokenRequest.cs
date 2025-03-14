﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers.Requests
{
    public class RefreshTokenRequest
    {
        [Required] public string Token { get; set; }
        [Required] public string RefreshToken { get; set; }
    }
}
