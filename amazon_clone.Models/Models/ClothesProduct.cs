namespace amazon_clone.Models.Models
{
    public class ClothesProduct : CustomerProduct
    {
        public int ClothesProductID { get; set; }


        public int TargetGenderID { get; set; }
        public PersonGender TargetGender { get; set; } = null!;


        public ICollection<ClothSize> Sizes { get; set; } = new List<ClothSize>();
        public ICollection<ClothesSizes> ClothesSizes { get; set; } = new List<ClothesSizes>();
    }
}
