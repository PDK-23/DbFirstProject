using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DbFirstProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var user = await _userService.GetByIdAsync(id);
        //    if (user == null) return NotFound();
        //    return Ok(user);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            var user = await _userService.CreateAsync(dto);
            if (user == null)
                return BadRequest("Username already exists!");
            return Ok(user);
        }
    }
}
