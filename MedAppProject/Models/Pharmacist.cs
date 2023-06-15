namespace MedAppProject.Models
{
    public class Pharmacist:User
    {
        public string Location { get; set; }
        public List<Prescription> Prescriptions { get; set; }
    }
}
