using EasyMarketing.Data;
using EasyMarketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EasyMarketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            // Validate if the user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists with this email." });
            }

            // Hash the password before saving
            var passwordHasher = new PasswordHasher<RegisterUserDto>();
            var hashedPassword = passwordHasher.HashPassword(registerDto, registerDto.Password);

            // Map DTO to User entity
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                Age = registerDto.Age,
                Phone = registerDto.Phone,
                Address = registerDto.Address,
                PinCode = registerDto.PinCode
            };

            // Save user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest(new { success = false, message = "Invalid request." });
            }

            // Query the database for the user
            var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            if (user == null)
            {
                return Unauthorized(new { success = false, message = "User not found." });
            }

            // Verify the password
            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return Unauthorized(new { success = false, message = "Invalid email or password." });
            }

            // Return success response
            return Ok(new
            {
                success = true,
                message = "Login successful.",
                token = "fake-jwt-token-for-demo"
            });
        }
    }

    // DTOs for Register and Login
    public class RegisterUserDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        [Required]
        public int Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public int PinCode { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
