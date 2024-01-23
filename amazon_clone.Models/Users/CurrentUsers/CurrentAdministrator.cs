using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Users.CurrentUsers
{
    public static class CurrentAdministrator
    {
        public static string? UserID { get; set; } = null!;
        public static string Name { get; set; } = null!;

        [EmailAddress]
        public static string Email { get; set; } = null!;

        [Phone]
        public static string PhoneNumber { get; set; } = null!;

        [Url]
        public static string ImageUrl { get; set; } = null!;



        public static void SetValues(string UserID, string Name, string Email, string PhoneNumber, string ImageUrl)
        {
            if (CurrentCustomer.UserID is not null)
            {
                throw new InvalidOperationException("The current customer is already exist");
            }

            CurrentCustomer.UserID = UserID;
            CurrentCustomer.Name = Name;
            CurrentCustomer.Email = Email;
            CurrentCustomer.PhoneNumber = PhoneNumber;
            CurrentCustomer.ImageUrl = ImageUrl;
        }

        public static void UnsetValues()
        {
            UserID = default!;
            Name = default!;
            Email = default!;
            PhoneNumber = default!;
            ImageUrl = default!;
        }
    }
}
