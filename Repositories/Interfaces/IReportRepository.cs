using RetailPosSystem.Models;

namespace RetailPosSystem.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<List<Sale>> GetSalesByDateAsync(DateTime date);

        Task<List<Product>> GetLowStockProductsAsync(int threshold);
    }
}
