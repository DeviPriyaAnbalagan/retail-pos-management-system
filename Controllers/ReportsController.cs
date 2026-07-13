using Microsoft.AspNetCore.Mvc;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("daily-sales")]
        public async Task<IActionResult> GetDailySalesReport([FromQuery] DateTime? date)
        {
            var reportDate = date ?? DateTime.UtcNow.Date;

            var report = await _reportService.GetDailySalesReportAsync(reportDate);

            return Ok(report);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts([FromQuery] int threshold = 5)
        {
            try
            {
                var products = await _reportService.GetLowStockProductsAsync(threshold);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
