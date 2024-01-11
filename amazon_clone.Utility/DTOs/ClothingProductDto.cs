namespace amazon_clone.Utility.DTOs
{
    public record ClothingProductDto : CustomerProductDto
    {
        public required int ClothingProductID { get; set; }
    }
}
