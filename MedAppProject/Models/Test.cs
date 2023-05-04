namespace MedAppProject.Models
{
    public class Test
    {
        public int Id { get; set; }
        public TestInfo TestInfo { get; set; }
        public string Result { get; set; }
        public Patient Patient { get; set; }
    }
}
