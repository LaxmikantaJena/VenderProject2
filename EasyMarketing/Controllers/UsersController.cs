using EasyMarketing.Models;
using EasyMarketing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyMarketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _service.RegisterUserAsync(dto);
            if (result == "Registration successful.")
                return Ok(result);

            return BadRequest(result);
        }
       
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var result = await _service.AddUserAsync(user);
            if (result == "User added successfully.")
                return Ok(result);

            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user != null)
                return Ok(user);

            return NotFound("User not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
