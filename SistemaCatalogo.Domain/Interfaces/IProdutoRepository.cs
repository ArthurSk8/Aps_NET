using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Application.Interfaces
{
    // Herda as operações CRUD genéricas.
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        // Método obrigatório para a busca dinâmica (AJAX) - Requisito 7
        Task<IEnumerable<Produto>> SearchProductsAsync(string searchTerm);
    }
}