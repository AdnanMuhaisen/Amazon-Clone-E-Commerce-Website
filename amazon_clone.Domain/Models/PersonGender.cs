namespace amazon_clone.Domain.Models
{
    public class PersonGender
    {
        public int GenderID { get; set; }
        public string Gender { get; set; } = null!;

        public ICollection<ClothingProduct> ClothesProducts { get; set; } = new List<ClothingProduct>();
    }
}
