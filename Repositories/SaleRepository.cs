using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RetailPosSystem.Data;
using RetailPosSystem.Models;
using RetailPosSystem.Repositories.Interfaces;

namespace RetailPosSystem.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task<Sale?> GetSaleByIdAsync(int saleId)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .Include(s => s.Payment)
                .FirstOrDefaultAsync(s => s.Id == saleId);
        }

        public async Task<Sale?> GetSaleByReceiptNumberAsync(string receiptNumber)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .Include(s => s.Payment)
                .FirstOrDefaultAsync(s => s.ReceiptNumber == receiptNumber);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task AddInventoryTransactionAsync(InventoryTransaction inventoryTransaction)
        {
            await _context.InventoryTransactions.AddAsync(inventoryTransaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
