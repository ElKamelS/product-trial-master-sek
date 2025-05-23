using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT.Domain.Entities;
using PROJECT.Domain.Models;
using PROJECT.Infrastructure.Data;
using System;
using System.Security.Claims;

namespace PROJECT_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public CartController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.email == email);
            if (user == null) return NotFound();

            var cartItems = user.Cart.Select(p => new ProductModel
            {
                id = p.id,
                name = p.name,
                price = p.price
            }).ToList();

            return Ok(cartItems);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.email == email);
            var product = await _context.Products.FindAsync(productId);

            if (user == null || product == null) return NotFound();

            user.Cart.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Added to cart");
        }
    }
}
