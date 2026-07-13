using RetailPosSystem.DTOs;

namespace RetailPosSystem.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();

        Task<ProductResponseDto?> GetProductByIdAsync(int id);

        Task<ProductResponseDto?> GetProductByBarcodeAsync(string barcode);

        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);

        Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteProductAsync(int id);

        Task<ProductSearchResponseDto> SearchProductsAsync(string? searchText, int pageNumber, int pageSize);
    }
}
