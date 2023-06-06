using Business.Abstract;
using Core.Results.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.ProductDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpPost("getProductById")]
        public async Task<IActionResult> GetProduct(ProductRequestDto productRequestDto)
        {
            if (productRequestDto==null) return NotFound("Product not found");
            var productDataResult=await _productService.GetAsync(productRequestDto.ProductId);
            
            if(productDataResult==null) return NotFound("Product not found");
            if (!productDataResult.IsSuccess) return NotFound(productDataResult.Message);

            var product = productDataResult.Data;
            return Ok(product);
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            return NotFound();

            var productsDataResult=await _productService.GetAllAsync();
            
            if (productsDataResult==null) return NotFound("Product not found");
            if(!productsDataResult.IsSuccess) return NotFound(productsDataResult.Message);

            var products = productsDataResult.Data;
            return Ok(products);
        }
    }
}
