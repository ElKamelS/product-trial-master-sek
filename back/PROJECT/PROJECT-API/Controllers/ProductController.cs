using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PROJECT.Application.Interfaces;
using PROJECT.Domain.Entities;
using System.Security.Claims;

namespace PROJECT_API.Controllers
{
    [ApiController]
    [Route("products")]
    [Authorize]
    public class ProductController(IProductService service) : ControllerBase
    {
        #region path: /products

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != "admin@admin.com")
                return Forbid("Only admin can add products");

            var created = await service.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = created.id }, created);
        }

        #endregion

        #region path: /products/id

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await service.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var product = await service.GetByIdAsync(id);
            if (product == null) return NotFound();

            patchDoc.ApplyTo(product, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await service.UpdateAsync(id, product);
            if (!updated) return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await service.DeleteAsync(id)) return NotFound();
            return Ok();
        }

        #endregion
    }
}
