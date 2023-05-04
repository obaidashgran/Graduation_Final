namespace MedAppProject.Models
{
    public class Appointment
    {
       
        public int Id { get; set; }
        public DateTime bookTime { get; set; }
        public Patient patient { get; set; }
    }
}
