using MedAppProject.Data;
using MedAppProject.Models;

namespace MedAppProject.Repositories
{
    public class LabAvailableTimesRepository
    {
        ApplicationDbContext db;
        public LabAvailableTimesRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(LabAvailableTimes entity)
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
            //var av = db.Find(id);
            //db.Remove(av);
            //db.SaveChanges();
        }

        public IEnumerable<DoctorAvailableTimes> GetAll()
        {
            throw new NotImplementedException();
        }

        public DoctorAvailableTimes GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DoctorAvailableTimes entity)
        {
            throw new NotImplementedException();
        }
    }
}
