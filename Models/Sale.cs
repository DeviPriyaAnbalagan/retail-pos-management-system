using System.Net.ServerSentEvents;

namespace RetailPosSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public string ReceiptNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public decimal SubTotal { get; set; }

        public decimal VatAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SaleItem> SaleItems { get; set; } = new();

        public Payment? Payment { get; set; }
    }
}
