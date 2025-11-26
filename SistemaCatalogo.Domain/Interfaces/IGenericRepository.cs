using System.Collections.Generic;
using System.Threading.Tasks;

// TEntity deve ser uma classe (a restrição 'where TEntity : class')
namespace SistemaCatalogo.Application.Interfaces
{
    // Interface genérica para as operações básicas de persistência (CRUD)
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}