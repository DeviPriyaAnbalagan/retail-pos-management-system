namespace RetailPosSystem.DTOs
{
    public class UpdateProductDto
    {
        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal VatPercentage { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
