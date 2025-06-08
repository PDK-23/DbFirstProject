using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.DTOs.User;
using DbFirstProject.Application.Interfaces;
using DbFirstProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbFirstProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user == null)
                return BadRequest("Username already exists!");
            return Ok(user);
        }

    }
}
