using api.Data;
using api.models;
using api.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using api.Interfaces;

namespace api.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {

            _productRepo = productRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.GetProductAsync();

            return Ok(products.Select((p) => p.ToProductDto()));

        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> FindProduct([FromRoute] int id)
        {

            var product = await _productRepo.FindProductByIdAsync(id);
            if (product == null) return NotFound("Please check entered id again!");

            return Ok(product);


        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var product = productDto.ToProductFromCreateDto();
            await _productRepo.CreateProductAsync(product);
            return Ok(product.ToProductDto());
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto updateDto)
        {
            var existingProduct = await _productRepo.UpdateProductAsync(id, updateDto);
            return Ok(existingProduct?.ToProductDto());



        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            await _productRepo.RemoveProductAsync(id);
            return NoContent();
        }
    }
}

