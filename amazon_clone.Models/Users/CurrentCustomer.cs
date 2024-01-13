﻿using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Users
{
    public static class CurrentCustomer
    {
        public static string? UserID { get; set; } = null!;
        public static string Name { get; set; } = null!;
        
        [EmailAddress]
        public static string Email { get; set; } = null!;

        [Phone]
        public static string PhoneNumber { get; set; } = null!;
        public static int? WishlistID { get; set; } = null!;

        public static void SetValues(string UserID,string Name,string Email,string PhoneNumber,int? WishlistID)
        {
            if(CurrentCustomer.UserID is not null)
            {
                throw new InvalidOperationException("The current customer is already exist");
            }

            CurrentCustomer.UserID = UserID;
            CurrentCustomer.Name = Name;
            CurrentCustomer.Email = Email;
            CurrentCustomer.PhoneNumber = PhoneNumber;
            CurrentCustomer.WishlistID = WishlistID;
        }

        public static void UnsetValues()
        {
            CurrentCustomer.UserID = default!;
            CurrentCustomer.Name = default!;
            CurrentCustomer.Email = default!;
            CurrentCustomer.PhoneNumber = default!;
            CurrentCustomer.WishlistID = default!;
        }
    }
}