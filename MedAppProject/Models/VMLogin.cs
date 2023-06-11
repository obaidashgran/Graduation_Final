namespace MedAppProject.Models
{
    public class VMLogin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
        public int userId { get; set; }
        public Role RoleId { get; set; }
        public bool isVerified { get; set; }
        public string code { get; set; }
    }
}
