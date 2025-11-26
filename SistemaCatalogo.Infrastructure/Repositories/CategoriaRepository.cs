using Microsoft.EntityFrameworkCore;
using SistemaCatalogo.Application.Interfaces; // Para ICategoriaRepository
using SistemaCatalogo.Domain.Entities; // Para Categoria
using SistemaCatalogo.Infrastructure.Context; // Para ApplicationDbContext
using System.Threading.Tasks;

namespace SistemaCatalogo.Infrastructure.Repositories
{
    // Herda o CRUD básico do GenericRepository e implementa métodos específicos
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            // O construtor apenas passa o contexto para a classe base
        }

        // Implementação do método específico GetCategoriaWithProdutosAsync
        public async Task<Categoria> GetCategoriaWithProdutosAsync(int id)
        {
            // Inclui a coleção de Produtos ao buscar a Categoria
            return await _dbSet
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}