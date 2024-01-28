using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Users.CurrentUsers;

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
        if (CurrentAdministrator.UserID is not null)
        {
            throw new InvalidOperationException("The current admin is already exist");
        }

        CurrentAdministrator.UserID = UserID;
        CurrentAdministrator.Name = Name;
        CurrentAdministrator.Email = Email;
        CurrentAdministrator.PhoneNumber = PhoneNumber;
        CurrentAdministrator.ImageUrl = ImageUrl;
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
