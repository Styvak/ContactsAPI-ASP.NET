using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Controllers.Requests;
using ContactsAPI.Controllers.Responses;
using ContactsAPI.Models;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Account created</response>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResult
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">User correctly logged in</response>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResult
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        /// <summary>
        /// Refresh user token
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Token correctly refreshed</response>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResult
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }
    }
}
