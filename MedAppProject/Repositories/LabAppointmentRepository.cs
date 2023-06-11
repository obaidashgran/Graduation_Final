using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class LabAppointmentRepository:IMedAppRepository<LabAppointment>
    {
        ApplicationDbContext _db;

        public LabAppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(LabAppointment entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(LabAppointment entity)
        {
            if (entity != null)
            {

                _db.Remove(entity);
                _db.SaveChanges();
            }
        }

        public IEnumerable<LabAppointment> GetAll()
        {
            var ap = _db.LabAppointments.Include(p => p.patient).Include(l => l.Lab).ToList();
            return ap;
        }

        public LabAppointment GetById(int id)
        {
            var ap = _db.LabAppointments.Include(a => a.patient).Include(a => a.Lab).SingleOrDefault(x => x.Id == id);
            return ap;
        }

        public void Update(LabAppointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
