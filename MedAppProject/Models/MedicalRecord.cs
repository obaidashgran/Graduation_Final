namespace MedAppProject.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string Descreption { get; set; }
        public DateTime Date { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
