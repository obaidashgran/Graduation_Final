using MedAppProject.Models;
namespace MedAppProject.ViewModels
{
    public class LabDashboardViewModel
    {
        public LabDashboardViewModel()
        {
            // Parameterless constructor
        }
        public IEnumerable<Lab> Labs { get; set; }
        public Patient Patient { get; set; }
    }
}
