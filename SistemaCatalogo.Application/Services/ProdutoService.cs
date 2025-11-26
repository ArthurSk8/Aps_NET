using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using SistemaCatalogo.Application.DTOs;
using SistemaCatalogo.Application.Interfaces;
using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Application.Services
{
    // Serviço de Aplicação para Produto, implementando a lógica de negócio
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoViewDTO>> GetAllProdutosAsync()
        {
            // Busca a lista de produtos (Entidade) e mapeia para DTOs
            var produtos = await _produtoRepository.GetAllAsync();
            
            // O mapeamento Mapster incluirá automaticamente o NomeCategoria
            return produtos.Adapt<IEnumerable<ProdutoViewDTO>>();
        }
        
        public async Task<ProdutoViewDTO> GetProdutoByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null) return null;
            
            return produto.Adapt<ProdutoViewDTO>();
        }

        public async Task<ProdutoViewDTO> AddProdutoAsync(ProdutoCreateUpdateDTO produtoDto)
        {
            // Mapeia DTO de entrada para Entidade de Domínio
            var produtoEntity = produtoDto.Adapt<Produto>();
            
            var newProduto = await _produtoRepository.AddAsync(produtoEntity);

            // Mapeia Entidade salva para DTO de visualização antes de retornar
            return newProduto.Adapt<ProdutoViewDTO>();
        }

        public async Task UpdateProdutoAsync(int id, ProdutoCreateUpdateDTO produtoDto)
        {
            var existingProduto = await _produtoRepository.GetByIdAsync(id);
            
            if (existingProduto != null)
            {
                // Mapeia as propriedades do DTO para a entidade existente
                produtoDto.Adapt(existingProduto);
                existingProduto.Id = id; 
                
                await _produtoRepository.UpdateAsync(existingProduto);
            }
        }

        public async Task DeleteProdutoAsync(int id)
        {
            await _produtoRepository.DeleteAsync(id);
        }

        // Requisito 7: Implementação da Busca Dinâmica
        public async Task<IEnumerable<ProdutoViewDTO>> SearchProdutosAsync(string searchTerm)
        {
            // O repositório lida com a lógica de busca (onde o EF Core faz o WHERE)
            var produtos = await _produtoRepository.SearchProductsAsync(searchTerm);
            
            // Mapeia o resultado (Entidade) para DTO (View)
            return produtos.Adapt<IEnumerable<ProdutoViewDTO>>();
        }
    }
}