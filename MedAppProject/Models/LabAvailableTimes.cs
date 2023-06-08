namespace MedAppProject.Models
{
    public class LabAvailableTimes
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Lab Lab { get; set; }
    }
}
