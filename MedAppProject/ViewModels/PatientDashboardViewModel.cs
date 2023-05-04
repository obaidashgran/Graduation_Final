using MedAppProject.Models;
namespace MedAppProject.ViewMoels
{
    public class PatientDashboardViewModel
    {
        public Patient PatientInfo { get; set; }
        public IEnumerable<Specialization> Specializations { get; set; }
        public Specialization SelectedSpecialization { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }

    }
}
