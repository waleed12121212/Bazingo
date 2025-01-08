using Bazingo_Application.Services;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bazingo_Application.DTOs.Products;

namespace Bazingo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts( )
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products.Select(p => new ProductDTO
            {
                ProductID = p.ProductID ,
                ProductName = p.ProductName ,
                Description = p.Description ,
                Price = p.Price ,
                Quantity = p.Quantity
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = new Product
            {
                ProductName = productCreateDTO.ProductName ,
                Description = productCreateDTO.Description ,
                Price = productCreateDTO.Price ,
                Quantity = productCreateDTO.Quantity
            };

            await _productService.AddProductAsync(product);
            return Ok(new { message = "Product created successfully." });
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetAllProductsAsync();
            var filtered = products.Where(p => p.CategoryID == categoryId);

            return Ok(filtered.Select(p => new ProductDTO
            {
                ProductID = p.ProductID ,
                ProductName = p.ProductName ,
                Description = p.Description ,
                Price = p.Price ,
                Quantity = p.Quantity
            }));
        }
    }
}
