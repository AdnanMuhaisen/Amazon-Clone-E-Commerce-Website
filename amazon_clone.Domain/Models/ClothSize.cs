namespace amazon_clone.Domain.Models
{
    public class ClothSize
    {
        public int SizeID { get; set; }

        public string Size { get; set; } = null!;

        public CreationDetails SizeCreationDetails { get; set; } = new CreationDetails();
        public ICollection<ClothingProduct> ClothesProducts { get; set; } = new List<ClothingProduct>();
        public ICollection<ClothesSizes> ClothesSizes { get; set; } = new List<ClothesSizes>();
    }
}
