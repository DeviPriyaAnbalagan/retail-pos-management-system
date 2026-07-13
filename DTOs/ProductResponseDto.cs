namespace RetailPosSystem.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal VatPercentage { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}
