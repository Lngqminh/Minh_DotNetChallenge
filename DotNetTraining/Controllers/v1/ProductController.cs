using Application.Settings;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : BaseV1Controller<ProductService, ApplicationSetting>
    {
        private readonly ProductService _productService;
        public ProductController(
                IServiceProvider services,
                IHttpContextAccessor httpContextAccessor
                )
                : base(services, httpContextAccessor)
        {
            this._productService = services.GetService<ProductService>()!;
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Success(products);
        }

        [HttpGet("getProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);
            return Success(product);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            return CreatedSuccess(await _productService.CreateProduct(product));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product, Guid id)
        {
            return Success(await _productService.UpdateProduct(product, id));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return Success("Product deleted successfully");
        }
    }
}
