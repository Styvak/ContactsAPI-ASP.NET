﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
