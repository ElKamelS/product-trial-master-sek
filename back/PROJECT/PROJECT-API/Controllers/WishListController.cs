using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT.Domain.Models;
using PROJECT.Infrastructure.Data;
using System.Security.Claims;

namespace PROJECT_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("wishlist")]
    public class WishListController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public WishListController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Wishlist).FirstOrDefaultAsync(u => u.email == email);
            if (user == null) return NotFound();

            var WLItems = user.Wishlist.Select(p => new ProductModel
            {
                id = p.id,
                name = p.name,
                price = p.price
            }).ToList();

            return Ok(WLItems);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToWishList(int productId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Wishlist).FirstOrDefaultAsync(u => u.email == email);
            var product = await _context.Products.FindAsync(productId);

            if (user == null || product == null) return NotFound();

            user.Wishlist.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Added to WishList");
        }
    }
}
