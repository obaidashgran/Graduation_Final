namespace MedAppProject.Models
{
    public class DoctorAvailableTimes
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Time { get; set; }

        public virtual Doctor Doctor { get; set; }

    }
}
