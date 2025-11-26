using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Application.Interfaces
{
    // Herda as operações CRUD genéricas.
    // Pode adicionar métodos específicos de Categoria se necessário (ex: GetCategoriasComProdutos)
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        // Exemplo de método específico para DDD, que traria a Categoria junto com seus Produtos
        Task<Categoria> GetCategoriaWithProdutosAsync(int id);
    }
}