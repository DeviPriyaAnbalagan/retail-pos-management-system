using Microsoft.EntityFrameworkCore;
using RetailPosSystem.Data;
using RetailPosSystem.Models;
using RetailPosSystem.Repositories.Interfaces;

namespace RetailPosSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();  
        }

        public async Task<Product?> GetByIdAsync(int id) 
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<Product?> GetByBarcodeAsync(string barcode)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode && p.IsActive);
        }

        public async Task<bool> BarcodeExistsAsync(string barcode)
        {
            return await _context.Products.AnyAsync(p => p.Barcode == barcode);
        }

        public async Task<List<Product>> SearchProductsAsync(string? searchText, int pageNumber, int pageSize)
        {
            var query = BuildSearchQuery(searchText);

            return await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetProductSearchCountAsync(string? searchText)
        {
            var query = BuildSearchQuery(searchText);

            return await query.CountAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<Product> BuildSearchQuery(string? searchText)
        {
            var query = _context.Products
                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var search = searchText.ToLower();

                query = query.Where(p =>
                    p.Name.ToLower().Contains(search) ||
                    p.Barcode.ToLower().Contains(search));
            }

            return query;
        }
    }
}
