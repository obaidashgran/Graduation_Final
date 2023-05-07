using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;


namespace MedAppProject.Repositories
{
    public class DoctorRepository:IMedAppRepository<Doctor>
    {
        ApplicationDbContext db;
        public DoctorRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Add(Doctor entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //var a = db.Find(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            var doc = db.Doctors.Include(d => d.AvailableTime);
            return doc;
            
        }

        public Doctor GetById(int id)
        {
            var doc = db.Doctors.Include(a => a.AvailableTime).Include(a=>a.Appointments).ThenInclude(p=>p.patient).SingleOrDefault(d=>d.Id==id);
            return doc;
            //throw new NotImplementedException();
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
