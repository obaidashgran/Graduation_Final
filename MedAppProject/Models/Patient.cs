namespace MedAppProject.Models
{
    public class Patient : User
    {
        public string? PatientLocation { get; set; }
        public List<LabAppointment>? LabAppointments { get; set; }
        public List<DoctorAppointment>? DoctorAppointments { get; set; }
        public List<Test>? Tests { get; set; }
        public List<Prescription>? Prescription { set; get; }
    }
}
