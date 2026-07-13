using Microsoft.EntityFrameworkCore.Storage;
using RetailPosSystem.Models;

namespace RetailPosSystem.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<Product?> GetProductByIdAsync(int productId);

        Task AddSaleAsync(Sale sale);

        Task<Sale?> GetSaleByIdAsync(int saleId);

        Task<Sale?> GetSaleByReceiptNumberAsync(string receiptNumber);

        void UpdateProduct(Product product);

        Task AddInventoryTransactionAsync(InventoryTransaction inventoryTransaction);

        Task SaveChangesAsync();
    }
}
