using ContactsAPI.Controllers.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.SwaggerExamples.Requests
{
    public class RefreshTokenRequestExample : IExamplesProvider<RefreshTokenRequest>
    {
        public RefreshTokenRequest GetExamples()
        {
            return new RefreshTokenRequest
            {
                RefreshToken = "13e79370-c541-4507-ba18-54fc60fca9a7",
                Token = "13e79370-c541-4507-ba18-54fc60fca9a7"
            };
        }
    }
}
