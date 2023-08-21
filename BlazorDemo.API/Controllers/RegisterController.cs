using BlazorDemo.Core.Server.Services.Authentication;
using BlazorDemo.Core.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.API.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase {
        private readonly AuthenticationService _authenticationService;

        public RegisterController(
            AuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var result = await _authenticationService.RegisterUser(model);

            return Ok(result);
        }
    }
}
