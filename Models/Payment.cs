namespace RetailPosSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public Sale? Sale { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal AmountPaid { get; set; }

        public string PaymentStatus { get; set; } = "Completed";

        public string? TransactionReference { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
