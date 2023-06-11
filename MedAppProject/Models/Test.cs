using System.ComponentModel.DataAnnotations.Schema;

namespace MedAppProject.Models
{
    public class Test
    {
        public int Id { get; set; }
        //public TestInfo TestInfo { get; set; }
        public string? Result { get; set; }
		
		public byte[] ResultFile  { get; set; }
		public Patient Patient { get; set; }
        public Lab Lab { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }
	}
}
