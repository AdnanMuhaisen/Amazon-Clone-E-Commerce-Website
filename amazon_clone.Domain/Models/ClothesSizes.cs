namespace amazon_clone.Domain.Models
{
    public class ClothesSizes
    {
        public int ClothesProductID { get; set; }
        public int ClothesSizeID { get; set; }

        public ClothingProduct ClothesProduct { get; set; } = null!;
        public ClothSize ClothesSize { get; set; } = null!;
    }
}
