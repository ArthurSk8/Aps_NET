using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaCatalogo.Application.DTOs;

namespace SistemaCatalogo.Application.Services
{
    // A interface do Serviço de Aplicação é o ponto de entrada para o Controller.
    // Ela usa apenas DTOs, mantendo o domínio e a apresentação desacoplados.
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewDTO>> GetAllCategoriasAsync();
        Task<CategoriaViewDTO> GetCategoriaByIdAsync(int id);
        Task<CategoriaViewDTO> AddCategoriaAsync(CategoriaCreateUpdateDTO categoriaDto);
        Task UpdateCategoriaAsync(int id, CategoriaCreateUpdateDTO categoriaDto);
        Task DeleteCategoriaAsync(int id);
    }
}