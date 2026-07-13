namespace RetailPosSystem.DTOs
{
    public class DailySalesReportDto
    {
        public DateTime Date { get; set; }

        public int TotalTransactions { get; set; }

        public decimal TotalSalesAmount { get; set; }

        public decimal TotalVatAmount { get; set; }
    }

    public class LowStockProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }
    }
}
