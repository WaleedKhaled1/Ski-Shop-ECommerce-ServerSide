using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type,string? sort)
        {
            var products = await _productRepository.GetProductsAsync(brand, type,sort);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductBrands()
        {
            var brands = await _productRepository.GetProductBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductTypes()
        {
            var types = await _productRepository.GetProductTypesAsync();
            return Ok(types);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
             _productRepository.AddProductAsync(product);

            if(await _productRepository.SaveChangesAsync())
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            return BadRequest("Failed to create product");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Cannot update product");
            }

            _productRepository.UpdateProductAsync(product);

            if (!await _productRepository.SaveChangesAsync())
                return BadRequest("Failed to update product");

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _productRepository.ProductExists(id) ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

             _productRepository.DeleteProductAsync(product);

            if(await _productRepository.SaveChangesAsync() ) return BadRequest("Failed to delete product");
            return NoContent();
        }
    }
}
