using System.ComponentModel.DataAnnotations.Schema;

namespace amazon_clone.Models.Models
{
    public class ClothesSizes
    {
        public int ClothesProductID { get; set; }
        public int ClothesSizeID { get; set; }

        public ClothesProduct ClothesProduct { get; set; } = null!;
        public ClothSize ClothesSize { get; set; } = null!;
    }
}
