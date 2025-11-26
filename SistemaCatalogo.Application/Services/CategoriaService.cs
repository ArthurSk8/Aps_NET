using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster; // Mapster é usado aqui!
using SistemaCatalogo.Application.DTOs;
using SistemaCatalogo.Application.Interfaces; // Para ICategoriaRepository
using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        // Injeção de Dependência da Interface de Repositório (Inversão de Controle)
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaViewDTO>> GetAllCategoriasAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            
            // Usa Mapster para mapear a Entidade (Domain) para o DTO (Application)
            return categorias.Adapt<IEnumerable<CategoriaViewDTO>>();
        }

        public async Task<CategoriaViewDTO> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
            {
                // Em um projeto real, lançaríamos uma exceção de "Não Encontrado"
                return null; 
            }
            
            // Mapeia Entidade para DTO de Visualização
            return categoria.Adapt<CategoriaViewDTO>();
        }

        public async Task<CategoriaViewDTO> AddCategoriaAsync(CategoriaCreateUpdateDTO categoriaDto)
        {
            // Mapeia DTO de Criação para Entidade (Domain)
            var categoriaEntity = categoriaDto.Adapt<Categoria>();
            
            var newCategoria = await _categoriaRepository.AddAsync(categoriaEntity);

            // Mapeia Entidade de volta para DTO de Visualização antes de retornar
            return newCategoria.Adapt<CategoriaViewDTO>();
        }

        public async Task UpdateCategoriaAsync(int id, CategoriaCreateUpdateDTO categoriaDto)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(id);
            
            if (existingCategoria != null)
            {
                // Mapeia as propriedades do DTO para a entidade existente
                categoriaDto.Adapt(existingCategoria);
                
                // Garante que o ID não seja alterado
                existingCategoria.Id = id; 
                
                await _categoriaRepository.UpdateAsync(existingCategoria);
            }
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            await _categoriaRepository.DeleteAsync(id);
        }
    }
}