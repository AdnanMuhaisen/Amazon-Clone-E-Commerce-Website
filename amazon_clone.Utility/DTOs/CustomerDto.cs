namespace amazon_clone.Utility.DTOs
{
    public record CustomerDto(string PersonID,string Name,string Email,int CustomerID)
        : PersonDto(PersonID, Name, Email)
    {
        public int CustomerID { get; set; } = CustomerID;
    }
}
