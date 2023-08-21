using BlazorDemo.API.Routes;
using BlazorDemo.Core.Server.Services.Authentication;
using BlazorDemo.Core.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.API.Controllers {
    
    [Route(ApiEndpoints.Authentication.AuthenticationController)]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(
            AuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
        }

        [Route(ApiEndpoints.Authentication.Register)]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var result = await _authenticationService.RegisterUser(model);

            return Ok(result);
        }
    }
}
