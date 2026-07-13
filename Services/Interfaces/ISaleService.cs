using RetailPosSystem.DTOs;

namespace RetailPosSystem.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto);

        Task<SaleResponseDto?> GetSaleByIdAsync(int saleId);

        Task<SaleResponseDto?> GetSaleByReceiptNumberAsync(string receiptNumber);
    }
}
