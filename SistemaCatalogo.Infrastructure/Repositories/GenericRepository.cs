using Microsoft.EntityFrameworkCore;
using SistemaCatalogo.Application.Interfaces; // Para IGenericRepository
using SistemaCatalogo.Infrastructure.Context; // Para ApplicationDbContext
using System.Collections.Generic;
using System.Threading.Tasks;

// TEntity deve ser uma classe (a restrição 'where TEntity : class')
namespace SistemaCatalogo.Infrastructure.Repositories
{
    // Classe genérica que implementa o CRUD básico para qualquer Entidade
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>(); // Obtém o DbSet específico para a entidade
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // Salva as alterações no banco
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // O AsNoTracking é usado para operações de leitura para melhorar a performance
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified; // Define o estado como modificado
            await _context.SaveChangesAsync();
        }
    }
}