using RetailPosSystem.DTOs;
using RetailPosSystem.Repositories.Interfaces;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<DailySalesReportDto> GetDailySalesReportAsync(DateTime date)
        {
            var sales = await _reportRepository.GetSalesByDateAsync(date);

            return new DailySalesReportDto
            {
                Date = date.Date,
                TotalTransactions = sales.Count,
                TotalSalesAmount = sales.Sum(s => s.TotalAmount),
                TotalVatAmount = sales.Sum(s => s.VatAmount)
            };
        }

        public async Task<List<LowStockProductDto>> GetLowStockProductsAsync(int threshold)
        {
            if (threshold < 0)
                throw new Exception("Threshold cannot be negative.");

            var products = await _reportRepository.GetLowStockProductsAsync(threshold);

            return products.Select(p => new LowStockProductDto
            {
                ProductId = p.Id,
                Barcode = p.Barcode,
                ProductName = p.Name,
                StockQuantity = p.StockQuantity
            }).ToList();
        }
    }
}
