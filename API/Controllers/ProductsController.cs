using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
  
    public class ProductsController(IGenericRepository<Product> productRepository) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec=new ProductSpecification(specParams);

            return await CreatePagination(productRepository, spec,specParams.PageNumber,specParams.PageSize);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
             productRepository.Add(product);

            if(await productRepository.SaveChangesAsync())
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            return BadRequest("Failed to create product");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
            return productRepository.IsExist(id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

             productRepository.Delete(product);

            if(!await productRepository.SaveChangesAsync() ) return BadRequest("Failed to delete product");
            return NoContent();
        }
    }
}
