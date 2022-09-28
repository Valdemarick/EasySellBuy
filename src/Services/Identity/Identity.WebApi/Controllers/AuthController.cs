using Identity.WebApi.Application.Models;
using Identity.WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (await _service.RegisterAsync(registerModel))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] LoginModel loginModel)
        {
            if (await _service.LoginAsync(loginModel))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}