namespace RetailPosSystem.DTOs
{
    public class CreateSaleDto
    {
        public List<CreateSaleItemDto> Items { get; set; } = new();

        public string PaymentMethod { get; set; } = "Cash";

        public decimal AmountPaid { get; set; }

        public string? TransactionReference { get; set; }
    }

    public class CreateSaleItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
