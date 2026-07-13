using Microsoft.EntityFrameworkCore;
using RetailPosSystem.Data;
using RetailPosSystem.Models;
using RetailPosSystem.Repositories.Interfaces;

namespace RetailPosSystem.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSalesByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate < endDate)
                .ToListAsync();
        }

        public async Task<List<Product>> GetLowStockProductsAsync(int threshold)
        {
            return await _context.Products
                .Where(p => p.IsActive && p.StockQuantity <= threshold)
                .ToListAsync();
        }
    }
}
