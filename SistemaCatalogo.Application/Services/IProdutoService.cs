using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaCatalogo.Application.DTOs;

namespace SistemaCatalogo.Application.Services
{
    // Interface do Serviço de Aplicação para Produto
    public interface IProdutoService
    {
        // CRUD Básico
        Task<IEnumerable<ProdutoViewDTO>> GetAllProdutosAsync();
        Task<ProdutoViewDTO> GetProdutoByIdAsync(int id);
        Task<ProdutoViewDTO> AddProdutoAsync(ProdutoCreateUpdateDTO produtoDto);
        Task UpdateProdutoAsync(int id, ProdutoCreateUpdateDTO produtoDto);
        Task DeleteProdutoAsync(int id);
        
        // Requisito 7: Método para a Busca Dinâmica (AJAX)
        Task<IEnumerable<ProdutoViewDTO>> SearchProdutosAsync(string searchTerm);
    }
}