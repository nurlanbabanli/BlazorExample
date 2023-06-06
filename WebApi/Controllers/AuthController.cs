using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService=authService;
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto==null) return BadRequest("User data is empty");

            var registerResult = await _authService.RegisterAsync(userRegisterDto);
            if (registerResult==null) return StatusCode(500, "Register user internal server error");
            if (registerResult.InternalServerError) return StatusCode(500, "Register user internal server error. "+registerResult.Message);
            if (!registerResult.IsSuccess) return NotFound(registerResult.Message);
            return Ok(registerResult.Data);
        }
    }
}
