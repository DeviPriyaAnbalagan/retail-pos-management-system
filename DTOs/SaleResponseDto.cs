namespace RetailPosSystem.DTOs
{
    public class SaleResponseDto
    {
        public int Id { get; set; }

        public string ReceiptNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VatAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal AmountPaid { get; set; }

        public List<SaleItemResponseDto> Items { get; set; } = new();
    }

    public class SaleItemResponseDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal VatAmount { get; set; }

        public decimal LineTotal { get; set; }
    }
}
