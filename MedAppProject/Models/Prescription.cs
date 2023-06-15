namespace MedAppProject.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public string MedName { get; set; }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }
        public string Location { get; set; }
        public Pharmacist Pharmacist { get; set; }
    }
}
