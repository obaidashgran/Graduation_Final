namespace MedAppProject.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime PaidOn { get; set; }
    }
}
