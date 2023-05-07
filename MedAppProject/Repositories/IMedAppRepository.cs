using MedAppProject.Models;
namespace MedAppProject.Repositories
{
    public interface IMedAppRepository<TEntity> where TEntity : class
    {
        Task<VMLogin> CheckCredentialsAsync(string email, string password);
        void Add(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        //TEntity getFirst(int id);

    }
}
