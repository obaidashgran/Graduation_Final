using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;


namespace MedAppProject.Repositories
{
    public class PatientRepository:IMedAppRepository<Doctor>
    {
        ApplicationDbContext db;
        public PatientRepository(ApplicationDbContext _db)
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

        public void Delete(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            var doc = db.Doctors.Include(d => d.AvailableTime);
            return doc;
            
        }

        public Doctor GetById(int id)
        {
            var doc = db.Doctors.Include(a => a.AvailableTime).SingleOrDefault(d=>d.Id==id);
            return doc;
            //throw new NotImplementedException();
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
