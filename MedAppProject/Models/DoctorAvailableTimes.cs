namespace MedAppProject.Models
{
    public class DoctorAvailableTimes
    {
        public int Id { get; set; }
        
        public DateTime Time { get; set; }

        public  Doctor Doctor { get; set; }

    }
}
