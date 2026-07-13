using RetailPosSystem.Models;
using RetailPosSystem.DTOs;
using RetailPosSystem.Repositories.Interfaces;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(MapToResponseDto).ToList();
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return MapToResponseDto(product);
        }

        public async Task<ProductResponseDto?> GetProductByBarcodeAsync(string barcode)
        {
            var product = await _productRepository.GetByBarcodeAsync(barcode);

            if (product == null)
                return null;

            return MapToResponseDto(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Barcode))
                throw new Exception("Barcode is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Product name is required.");

            if (dto.Price <= 0)
                throw new Exception("Price must be greater than zero.");

            if (dto.VatPercentage < 0)
                throw new Exception("VAT percentage cannot be negative.");

            if (dto.StockQuantity < 0)
                throw new Exception("Stock quantity cannot be negative.");

            var barcodeExists = await _productRepository.BarcodeExistsAsync(dto.Barcode);

            if (barcodeExists)
                throw new Exception("Barcode already exists.");

            var product = new Product
            {
                Barcode = dto.Barcode,
                Name = dto.Name,
                Price = dto.Price,
                VatPercentage = dto.VatPercentage,
                StockQuantity = dto.StockQuantity,
                IsActive = true
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return MapToResponseDto(product);
        }

        public async Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            if (string.IsNullOrWhiteSpace(dto.Barcode))
                throw new Exception("Barcode is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Product name is required.");

            if (dto.Price <= 0)
                throw new Exception("Price must be greater than zero.");

            if (dto.VatPercentage < 0)
                throw new Exception("VAT percentage cannot be negative.");

            if (dto.StockQuantity < 0)
                throw new Exception("Stock quantity cannot be negative.");

            product.Barcode = dto.Barcode;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.VatPercentage = dto.VatPercentage;
            product.StockQuantity = dto.StockQuantity;
            product.IsActive = dto.IsActive;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return MapToResponseDto(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            product.IsActive = false;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }

        private static ProductResponseDto MapToResponseDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Barcode = product.Barcode,
                Name = product.Name,
                Price = product.Price,
                VatPercentage = product.VatPercentage,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive
            };
        }

    }
}
