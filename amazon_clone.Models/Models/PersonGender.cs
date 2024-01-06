namespace amazon_clone.Models.Models
{
    public class PersonGender
    {
        public int GenderID { get; set; }
        public string Gender { get; set; } = null!;

        public ICollection<ClothesProduct> ClothesProducts { get; set; } = new List<ClothesProduct>();
    }
}
