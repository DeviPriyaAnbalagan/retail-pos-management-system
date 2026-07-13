namespace RetailPosSystem.DTOs
{
    public class ProductSearchResponseDto
    {
        public List<ProductResponseDto> Products { get; set; } = new();

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
