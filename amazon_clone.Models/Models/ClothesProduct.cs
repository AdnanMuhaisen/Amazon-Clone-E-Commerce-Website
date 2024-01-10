namespace amazon_clone.Models.Models
{
    public class ClothesProduct : CustomerProduct
    {
        public int ClothesProductID { get; set; }


        public int TargetGenderID { get; set; }
        public PersonGender TargetGender { get; set; } = null!;


        public ICollection<ClothSize> Sizes { get; set; } = new List<ClothSize>();
        public ICollection<ClothesSizes> ClothesSizes { get; set; } = new List<ClothesSizes>();

        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(nameof(obj));
            if (obj is not ClothesProduct) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetObj = obj as ClothesProduct;
            return base.Equals(obj)
                && this.ClothesProductID == targetObj!.ClothesProductID
                && this.TargetGenderID == targetObj.TargetGenderID;
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash *= 7 + this.ClothesProductID.GetHashCode();
            hash *= 7 + this.TargetGenderID.GetHashCode();
            return hash;
        }
    }
}
