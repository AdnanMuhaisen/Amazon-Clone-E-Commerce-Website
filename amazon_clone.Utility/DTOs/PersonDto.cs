using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Utility.DTOs
{
    public record PersonDto(string PersonID,string Name,string Email)
    {
        public string PersonID { get; set; } = PersonID;
        public string Name { get; set; } = Name;

        [EmailAddress]
        public string Email { get; set; } = Email;
    
    }
}
