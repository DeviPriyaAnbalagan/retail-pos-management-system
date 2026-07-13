using RetailPosSystem.Models;

namespace RetailPosSystem.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetByBarcodeAsync(string barcode);

        Task<bool> BarcodeExistsAsync(string barcode);

        Task<List<Product>> SearchProductsAsync(string? searchText, int pageNumber, int pageSize);

        Task<int> GetProductSearchCountAsync(string? searchText);

        Task AddAsync(Product product);

        void Update(Product product);

        Task SaveChangesAsync();
    }
}
