using MedAppProject.Models;
namespace MedAppProject.ViewModels
{
    public class LabDashboardViewModel
    {
        public List<Lab>? Labs { get; set; }
        public Patient Patient { get; set; }
    }
}
