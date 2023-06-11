using MedAppProject.Data;
using MedAppProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MedAppProject.Repositories
{
    public class MedAppRepository<TEntity> : IMedAppRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        
        DbSet<TEntity> _entity = null;
        public MedAppRepository(ApplicationDbContext context)
        {
             this._context = context;
             this._entity = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _entity.Add(entity);
            _context.SaveChanges();
        }

        public async Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            var user = await _context.VMLogins.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            // If no user was found with the specified email, return false
            //if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            //{
            //    return user;
            //}

            return user;
        }

        public void Delete(TEntity entity)
        {
            //var entit = _entity.Find(entity);
            if (entity != null)
            {
                
                _entity.Remove(entity);
                _context.SaveChanges();
            }
            
        }
        

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public TEntity GetById(int id)
        {
            return _entity.Find(id);
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
            _context.SaveChanges();
        }
    }
}
