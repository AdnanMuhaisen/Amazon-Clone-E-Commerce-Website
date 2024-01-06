namespace amazon_clone.Models.Models
{
    public class ClothSize
    {
        public int SizeID { get; set; }

        public string Size { get; set; } = null!;

        public CreationDetails SizeCreationDetails { get; set; } = new CreationDetails();
        public ICollection<ClothesProduct> ClothesProducts { get; set; } = new List<ClothesProduct>();
        public ICollection<ClothesSizes> ClothesSizes { get; set; } = new List<ClothesSizes>();
    }
}
