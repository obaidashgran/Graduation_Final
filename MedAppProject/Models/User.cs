using System.ComponentModel.DataAnnotations;

namespace MedAppProject.Models
{
    public class User : Entity<int>
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
