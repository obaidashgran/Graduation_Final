using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class VMLoginRepository
    {

        ApplicationDbContext db;
        public VMLoginRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public void Add(VMLogin entity)
        {
            db.VMLogins.Add(entity);
        }

        public async Task<int> CheckCredentialsAsync(string email, string password)
        {
            var user = await db.VMLogins.FirstOrDefaultAsync(u => u.Email == email && u.Password==password);

            // If no user was found with the specified email, return false
            if (user == null)
            {
                return 0;
            }
            return user.userId;
        }

        public VMLogin getFirst(int id)
        {
            var lo = db.VMLogins.SingleOrDefault(index => index.Id == id);
            return lo;
        }
    }
}
