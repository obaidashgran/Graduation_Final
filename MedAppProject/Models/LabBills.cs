namespace MedAppProject.Models
{
	public class LabBills
	{
        public int Id { get; set; }
        public double Amount { get; set; }
        public Lab Lab { get; set; }
        public Patient Patient { get; set; }
        public DateTime PaidOn { get; set; }
    }
}
