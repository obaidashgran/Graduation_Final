using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class DoctorAppointmentRepository : IMedAppRepository<DoctorAppointment>
    {
        
        ApplicationDbContext _db;

        public DoctorAppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(DoctorAppointment entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(DoctorAppointment entity)
        {
            if (entity != null)
            {

                _db.Remove(entity);
                _db.SaveChanges();
            }
        }

        public IEnumerable<DoctorAppointment> GetAll()
        {
            var ap = _db.DoctorAppointments.Include(a => a.patient).Include(a => a.Doctor).ThenInclude(d=>d.DoctorSpecialization).ToList();
            return ap;
        }

        public DoctorAppointment GetById(int id)
        {
            var ap = _db.DoctorAppointments.Include(a => a.patient).Include(a => a.Doctor).ThenInclude(d=>d.DoctorSpecialization).SingleOrDefault(x => x.Id == id);
            return ap;
        }

        public void Update(DoctorAppointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
