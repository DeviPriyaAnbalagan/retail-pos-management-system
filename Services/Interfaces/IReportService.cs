using RetailPosSystem.DTOs;

namespace RetailPosSystem.Services.Interfaces
{
    public interface IReportService
    {
        Task<DailySalesReportDto> GetDailySalesReportAsync(DateTime date);

        Task<List<LowStockProductDto>> GetLowStockProductsAsync(int threshold);
    }
}
