namespace amazon_clone.Domain.Models
{
    public class ClothingProduct : CustomerProduct
    {
        public int ClothingProductID { get; set; }
        public int TargetGenderID { get; set; }
        public PersonGender TargetGender { get; set; } = null!;


        public ICollection<ClothSize> Sizes { get; set; } = new List<ClothSize>();
        public ICollection<ClothesSizes> ClothesSizes { get; set; } = new List<ClothesSizes>();

        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(nameof(obj));
            if (obj is not ClothingProduct) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetObj = obj as ClothingProduct;
            return base.Equals(obj)
                && ClothingProductID == targetObj!.ClothingProductID
                && TargetGenderID == targetObj.TargetGenderID;
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash *= 7 + ClothingProductID.GetHashCode();
            hash *= 7 + TargetGenderID.GetHashCode();
            return hash;
        }
    }
}
