
using RetailPosSystem.DTOs;
using RetailPosSystem.Models;
using RetailPosSystem.Repositories.Interfaces;
using RetailPosSystem.Services.Interfaces;

namespace RetailPosSystem.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("Sale must contain at least one item.");

            await using var transaction = await _saleRepository.BeginTransactionAsync();

            try
            {
                var sale = new Sale
                {
                    ReceiptNumber = GenerateReceiptNumber(),
                    SaleDate = DateTime.UtcNow
                };

                decimal subTotal = 0;
                decimal vatTotal = 0;

                foreach (var item in dto.Items)
                {
                    if (item.Quantity <= 0)
                        throw new Exception("Quantity must be greater than zero.");

                    var product = await _saleRepository.GetProductByIdAsync(item.ProductId);

                    if (product == null)
                        throw new Exception($"Product with ID {item.ProductId} not found.");

                    if (product.StockQuantity < item.Quantity)
                        throw new Exception($"Insufficient stock for product: {product.Name}");

                    var lineSubTotal = product.Price * item.Quantity;
                    var lineVat = lineSubTotal * product.VatPercentage / 100;
                    var lineTotal = lineSubTotal + lineVat;

                    var saleItem = new SaleItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        VatAmount = lineVat,
                        LineTotal = lineTotal
                    };

                    sale.SaleItems.Add(saleItem);

                    product.StockQuantity -= item.Quantity;
                    _saleRepository.UpdateProduct(product);

                    var inventoryTransaction = new InventoryTransaction
                    {
                        ProductId = product.Id,
                        QuantityChanged = -item.Quantity,
                        TransactionType = "Sale",
                        Reason = $"Stock reduced for sale {sale.ReceiptNumber}",
                        CreatedDate = DateTime.UtcNow
                    };

                    await _saleRepository.AddInventoryTransactionAsync(inventoryTransaction);

                    subTotal += lineSubTotal;
                    vatTotal += lineVat;
                }

                sale.SubTotal = subTotal;
                sale.VatAmount = vatTotal;
                sale.TotalAmount = subTotal + vatTotal;

                if (dto.AmountPaid < sale.TotalAmount)
                    throw new Exception("Amount paid is less than total amount.");

                sale.Payment = new Payment
                {
                    PaymentMethod = dto.PaymentMethod,
                    AmountPaid = dto.AmountPaid,
                    PaymentStatus = "Completed",
                    TransactionReference = dto.TransactionReference,
                    PaymentDate = DateTime.UtcNow
                };

                await _saleRepository.AddSaleAsync(sale);
                await _saleRepository.SaveChangesAsync();

                await transaction.CommitAsync();

                return MapToSaleResponseDto(sale);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<SaleResponseDto?> GetSaleByIdAsync(int saleId)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId);

            if (sale == null)
                return null;

            return MapToSaleResponseDto(sale);
        }

        public async Task<SaleResponseDto?> GetSaleByReceiptNumberAsync(string receiptNumber)
        {
            var sale = await _saleRepository.GetSaleByReceiptNumberAsync(receiptNumber);

            if (sale == null)
                return null;

            return MapToSaleResponseDto(sale);
        }

        private static string GenerateReceiptNumber()
        {
            return $"POS-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
        }

        private static SaleResponseDto MapToSaleResponseDto(Sale sale)
        {
            return new SaleResponseDto
            {
                Id = sale.Id,
                ReceiptNumber = sale.ReceiptNumber,
                SaleDate = sale.SaleDate,
                SubTotal = sale.SubTotal,
                VatAmount = sale.VatAmount,
                TotalAmount = sale.TotalAmount,
                PaymentMethod = sale.Payment?.PaymentMethod ?? string.Empty,
                AmountPaid = sale.Payment?.AmountPaid ?? 0,
                Items = sale.SaleItems.Select(item => new SaleItemResponseDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? string.Empty,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    VatAmount = item.VatAmount,
                    LineTotal = item.LineTotal
                }).ToList()
            };
        }
    }
}
