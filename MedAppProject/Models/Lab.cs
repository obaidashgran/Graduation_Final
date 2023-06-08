namespace MedAppProject.Models
{
    public class Lab : User
    {
        public string LabLocation { get; set; }
        public List<LabAvailableTimes> AvailableTimes { get; set; }
        public List<LabAppointment> LabAppointments { get; set; }
        public List<TestInfo> TestsInfo { get; set; }
    }
}
