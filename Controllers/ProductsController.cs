using Microsoft.AspNetCore.Mvc;
using RetailPosSystem.DTOs;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var product = await _productService.GetProductByBarcodeAsync(barcode);

            if (product == null)
                return NotFound("Product not found for this barcode.");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            try
            {
                var product = await _productService.CreateProductAsync(dto);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            try
            {
                var product = await _productService.UpdateProductAsync(id, dto);

                if (product == null)
                    return NotFound("Product not found.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
                return NotFound("Product not found.");

            return NoContent();
        }
    }
}
