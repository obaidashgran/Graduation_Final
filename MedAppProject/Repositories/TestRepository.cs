using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
	public class TestRepository : IMedAppRepository<Test>
	{
		ApplicationDbContext _db;

		public TestRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public void Add(Test entity)
		{
			_db.Add(entity);
			_db.SaveChanges();
		}

		public Task<VMLogin> CheckCredentialsAsync(string email, string password)
		{
			throw new NotImplementedException();
		}

		public void Delete(Test entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Test> GetAll()
		{
			var test = _db.Tests.ToList();
			return test;
		}

		public Test GetById(int id)
		{
			var test = _db.Tests.SingleOrDefault(a => a.Id == id);
			return test;
		}

		public void Update(Test entity)
		{
			throw new NotImplementedException();
		}
	}
}
