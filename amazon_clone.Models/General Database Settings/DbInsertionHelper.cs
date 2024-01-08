using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Models.General_Database_Settings
{
    [Owned]
    public class DbInsertionHelper
    {
        public int LastInsertedCustomerID { get; set; }
    }
}
