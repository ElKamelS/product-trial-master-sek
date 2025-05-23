using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT.Application.Services;
using PROJECT.Domain.Entities;
using PROJECT.Domain.Models;
using PROJECT.Infrastructure.Data;
using System;

namespace PROJECT_API.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ProjectDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("account")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (_context.Users.Any(u => u.email == model.email))
                return BadRequest("Email already used");

            var user = new User
            {
                username = model.username,
                firstname = model.firstname,
                email = model.email,
                password = BCrypt.Net.BCrypt.HashPassword(model.password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created");
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
                                            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == model.email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.password))
                return Unauthorized("Invalid credentials");

            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }
    }
}
