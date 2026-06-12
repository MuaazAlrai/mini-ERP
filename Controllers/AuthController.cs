using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniERP.DTOs;
using MiniERP.Models;
using MiniERP.Services;

namespace MiniERP.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwt;

        public AuthController(UserManager<AppUser> userManager, JwtService jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        // ---------------- REGISTER ----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Invalid request data"
                });
            }

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => e.Description)
                    .ToList();

                return BadRequest(new
                {
                    message = "Registration failed",
                    errors
                });
            }

            return Ok(new
            {
                message = "User registered successfully"
            });
        }

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Invalid request data"
                });
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            var token = _jwt.GenerateToken(user);

            return Ok(new
            {
                message = "Login successful",
                token
            });
        }
    }
}