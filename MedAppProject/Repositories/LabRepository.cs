using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class LabRepository : IMedAppRepository<Lab>
    {
        ApplicationDbContext db;
        public LabRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Add(Lab entity)
        {
            throw new NotImplementedException();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(Lab entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lab> GetAll()
        {
            var lab = db.Labs.Include(t=>t.TestsInfo).Include(a=>a.AvailableTimes).Include(l => l.LabAppointments);
            return lab;
        }

        public Lab GetById(int id)
        {
            var lab = db.Labs.Include(t=>t.TestsInfo).Include(a=>a.AvailableTimes).Include(l=>l.LabAppointments).ThenInclude(p=>p.patient).SingleOrDefault(d => d.Id == id);
            return lab;
        }

        public void Update(Lab entity)
        {
            throw new NotImplementedException();
        }
    }
}
