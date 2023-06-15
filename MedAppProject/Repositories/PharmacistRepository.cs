using MedAppProject.Data;
using MedAppProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
	public class PharmacistRepository : IMedAppRepository<Pharmacist>
	{
		ApplicationDbContext _db;

		public PharmacistRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Add(Pharmacist entity)
		{
			_db.Add(entity);
			_db.SaveChanges();
		}

		public Task<VMLogin> CheckCredentialsAsync(string email, string password)
		{
			throw new NotImplementedException();
		}

		public void Delete(Pharmacist entity)
		{
			if (entity != null)
			{

				_db.Remove(entity);
				_db.SaveChanges();
			}
		}

		public IEnumerable<Pharmacist> GetAll()
		{
			var pa = _db.Pharmacies.Include(a => a.Prescriptions).ToList();
			return pa;
		}

		public Pharmacist GetById(int id)
		{
			var pha = _db.Pharmacies.Include(a=>a.Prescriptions).SingleOrDefault(p=>p.Id==id);
			return pha;
		}

		public void Update(Pharmacist entity)
		{
			var existingPharmacist = _db.Pharmacies.Find(entity.Id);
			if (existingPharmacist != null)
			{
				// Update the properties of the existingPharmacist entity
				existingPharmacist.Location = entity.Location;
				existingPharmacist.Prescriptions = entity.Prescriptions;

				_db.SaveChanges();
			}
		}
	}
}
