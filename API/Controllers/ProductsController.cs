using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specification;
using Core.Specifications;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type,string? sort)
        {
            var spec=new ProductSpecification(brand, type, sort);
             
            var products = await productRepository.ListWithSpecAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductBrands()
        {
             var BrandSpec = new BrandListSpecification();

            var brands = await productRepository.ListWithSpecAsync(BrandSpec);
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductTypes()
        {
             var TypeSpec = new TypeListSpecification();

             var types = await productRepository.ListWithSpecAsync(TypeSpec);
            return Ok(types);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
             productRepository.Add(product);

            if(await productRepository.SaveChangesAsync())
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

            productRepository.Update(product);

            if (!await productRepository.SaveChangesAsync())
                return BadRequest("Failed to update product");

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return productRepository.IsExist(id) ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

             productRepository.Delete(product);

            if(await productRepository.SaveChangesAsync() ) return BadRequest("Failed to delete product");
            return NoContent();
        }
    }
}
