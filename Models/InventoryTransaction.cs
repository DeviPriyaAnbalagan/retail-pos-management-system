namespace RetailPosSystem.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int QuantityChanged { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
