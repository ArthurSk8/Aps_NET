using Microsoft.EntityFrameworkCore;
using SistemaCatalogo.Application.Interfaces; // Para IProdutoRepository
using SistemaCatalogo.Domain.Entities; // Para Produto
using SistemaCatalogo.Infrastructure.Context; // Para ApplicationDbContext
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCatalogo.Infrastructure.Repositories
{
    // Herda o CRUD básico do GenericRepository e implementa métodos específicos
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context) : base(context)
        {
            // O construtor apenas passa o contexto para a classe base
        }

        // Implementação do método de busca dinâmica (Requisito 7)
        public async Task<IEnumerable<Produto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Se a busca estiver vazia, retorna todos os produtos (opcional)
                return await _dbSet.AsNoTracking().Include(p => p.Categoria).ToListAsync();
            }

            var lowerSearchTerm = searchTerm.ToLower();

            // Filtra produtos cujo nome ou descrição contenham o termo de busca
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Categoria) // Inclui a categoria para exibição
                .Where(p => p.Nome.ToLower().Contains(lowerSearchTerm) || 
                            p.Descricao.ToLower().Contains(lowerSearchTerm))
                .ToListAsync();
        }
    }
}