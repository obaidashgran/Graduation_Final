using System.ComponentModel.DataAnnotations;

namespace MedAppProject.Models
{
    public class Entity<T>
    {
        
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
