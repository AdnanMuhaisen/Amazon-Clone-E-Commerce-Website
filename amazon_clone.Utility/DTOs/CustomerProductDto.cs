namespace amazon_clone.Utility.DTOs
{
    public record CustomerProductDto
    {
        public required int ProductID { get; set; }
        public required int CustomerProductID { get; set; }
    }
}
