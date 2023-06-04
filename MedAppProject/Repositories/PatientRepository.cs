using MedAppProject.Data;
using MedAppProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class PatientRepository : IMedAppRepository<Patient>
    {
        ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Patient entity)
        {
            throw new NotImplementedException();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            var pa = _db.Patients.Include(p => p.DoctorAppointments).ThenInclude(d => d.Doctor).ToList();
            return pa;
        }

        public Patient GetById(int id)
        {
            var pa = _db.Patients.Include(p=>p.Prescription).Include(p => p.DoctorAppointments).ThenInclude(d=>d.Doctor).SingleOrDefault(a=>a.Id==id);
            return pa;
        
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
