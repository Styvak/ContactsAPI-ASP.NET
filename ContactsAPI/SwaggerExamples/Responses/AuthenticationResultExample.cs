using ContactsAPI.Controllers.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Responses
{
    public class AuthenticationResultExample : IExamplesProvider<AuthenticationResult>
    {
        public AuthenticationResult GetExamples()
        {
            return new AuthenticationResult
            {
                Success = true,
                Token = "token",
                RefreshToken = "Refresh token",
                Errors = null
            };
        }
    }
}
