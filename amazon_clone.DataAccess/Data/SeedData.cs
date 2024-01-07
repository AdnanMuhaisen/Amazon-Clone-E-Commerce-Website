using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Data
{
    public static class SeedData
    {
        public static List<ProductCategory> LoadCategories() =>
            new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    CategoryName="Electronics"
                },
                new ProductCategory()
                {
                    CategoryName="Jewelery"
                },
                new ProductCategory()
                {
                    CategoryName="Men's Clothing"
                },
                new ProductCategory()
                {
                    CategoryName="Women's Clothing"
                }
            };

        public static List<PersonGender> LoadGenders() =>
            new List<PersonGender>()
            {
                new PersonGender()
                {
                    Gender="Male"
                },
                new PersonGender()
                {
                    Gender="Female"
                }
            };

        public static List<ClothSize> LoadClothesSizes() =>
            new List<ClothSize>()
            {
                new ClothSize()
                {
                    SizeID=1,
                    Size="S"
                },
                new ClothSize()
                {
                    SizeID=2,
                    Size="M"
                },
                new ClothSize()
                {SizeID = 3,
                    Size="L"
                },
                new ClothSize()
                {SizeID = 4,
                    Size="XL"
                },
                new ClothSize()
                {SizeID = 5,
                    Size="XXL"
                }
            };

        public static List<CustomerProduct> LoadCustomerProducts() =>
            new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    Name="WD 2TB Elements Portable External Hard Drive - USB 3.0",
                    Description="USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance " +
                    "High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be " +
                    "required for other operating systems;" +
                    " Compatibility may vary depending on user’s hardware configuration and operating system",
                    Price=64.0m,
                    Quantity=10,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\e1.jpg",
                    CategoryID=1
                },
                new CustomerProduct()
                {
                    Name="Samsung 49-Inch CHG90 144Hz Curved Gaming Monitor (LC49HG90DMNXZA) – Super Ultrawide Screen QLED",
                    Description="49 INCH SUPER ULTRAWIDE 32:9 CURVED GAMING MONITOR with dual 27 inch screen side by side QUANTUM DOT (QLED) TECHNOLOGY, HDR support and factory calibration provides stunningly realistic and accurate color and contrast 144HZ HIGH REFRESH RATE and 1ms ultra fast response time work to eliminate motion blur, ghosting, and reduce input lag",
                    Price=999.9m,
                    Quantity=7,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\e2.jpg",
                    CategoryID=1
                },
                new CustomerProduct()
                {
                    Name="John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet",
                    Description="From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection.",
                    Price=695.0m,
                    Quantity=3,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\j1.jpg",
                    CategoryID=2
                },
                new CustomerProduct()
                {
                    Name="White Gold Plated Princess",
                    Description="Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day...",
                    Price=9.99m,
                    Quantity=5,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\j2.jpg",
                    CategoryID=2
                }
            };

        public static List<ClothesProduct> LoadClothesProducts() =>
            new List<ClothesProduct>()
            {
                 new ClothesProduct()
                {
                    Name="Mens Casual Premium Slim Fit T-Shirts",
                    Description="Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.",
                    Price=22.3m,
                    Quantity=100,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\cm1.jpg",
                    CategoryID=3,
                    TargetGenderID=1,
                },
                new ClothesProduct()
                {
                    Name="Mens Cotton Jacket",
                    Description="great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.",
                    Price=55.9m,
                    Quantity=100,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\cm2.jpg",
                    CategoryID=3,
                    TargetGenderID=1
                },
                new ClothesProduct()
                {
                    Name="BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats",
                    Description="Note:The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.Stand Collar Liner jacket, keep you warm in cold weather. Zippered Pockets: 2 Zippered Hand Pockets, 2 Zippered Pockets on Chest (enough to keep cards or keys)and 1 Hidden Pocket Inside.Zippered Hand Pockets and Hidden Pocket keep your things secure. Humanized Design: Adjustable and Detachable Hood and Adjustable cuff to prevent the wind and water,for a comfortable fit. 3 in 1 Detachable Design provide more convenience, you can separate the coat and inner as needed, or wear it together. It is suitable for different season and help you adapt to different climates",
                    Price=56.9m,
                    Quantity=34,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\cw1.jpg",
                    CategoryID=4,
                    TargetGenderID=2
                },
                new ClothesProduct()
                {
                    Name="Rain Jacket Women Windbreaker Striped Climbing Raincoats",
                    Description="Lightweight perfet for trip or casual wear---Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets are a good size to hold all kinds of things, it covers the hips, and the hood is generous but doesn't overdo it.Attached Cotton Lined Hood with Adjustable Drawstrings give it a real styled look.",
                    Price=39.99m,
                    Quantity=34,
                    ImageUrl=@"C:\Users\ADNAN MUHAISEN\Pictures\cw2.jpg",
                    CategoryID=4,
                    TargetGenderID=2
                }
            };

        public static List<OrderStatus> LoadOrderStatuses() =>
            new List<OrderStatus>()
            {
                //processing, shipped, delivered
                new OrderStatus()
                {
                    Status="PROCESSING"
                },
                new OrderStatus()
                {
                    Status="SHIPPED"
                },
                new OrderStatus()
                {
                    Status="DELIVERED"
                }
            };

        public static List<PromoCode> LoadPromoCodes() =>
            new List<PromoCode>()
            {
                new PromoCode()
                {
                    Code="SHUBHO20",
                    ForQuantity=2,
                },
                new PromoCode()
                {
                    Code="SHUBHO30",
                    ForQuantity=3,
                },
                new PromoCode()
                {
                    // This promo code is for the quantity of the number 4 and more than 4
                    Code="SHUBHO40",
                    ForQuantity=4,
                }
            };
    }
}
