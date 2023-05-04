namespace MedAppProject.Models
{
    public class Doctor : User
    {
        public string ClincLocation { get; set; }
        public Specialization DoctorSpecialization { get; set; }
        public double Fees { get; set; }
        //public string ClincLocation { get; set; }
        public List<DoctorAvailableTimes>? AvailableTime { get; set; }  
        public List<DoctorAppointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { set; get; }
        public double DoctorRate { get; set; }
    }
}
