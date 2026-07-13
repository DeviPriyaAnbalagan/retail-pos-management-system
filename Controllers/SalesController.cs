using Microsoft.AspNetCore.Mvc;
using RetailPosSystem.DTOs;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDto dto)
        {
            try
            {
                var sale = await _saleService.CreateSaleAsync(dto);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);

            if (sale == null)
                return NotFound("Sale not found.");

            return Ok(sale);
        }

        [HttpGet("receipt/{receiptNumber}")]
        public async Task<IActionResult> GetSaleByReceiptNumber(string receiptNumber)
        {
            var sale = await _saleService.GetSaleByReceiptNumberAsync(receiptNumber);

            if (sale == null)
                return NotFound("Sale not found for this receipt number.");

            return Ok(sale);
        }
    }
}
