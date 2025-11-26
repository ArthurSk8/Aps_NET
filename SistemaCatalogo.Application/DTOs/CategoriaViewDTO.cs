using System.ComponentModel.DataAnnotations;

namespace SistemaCatalogo.Application.DTOs
{
    public class CategoriaViewDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Total de Produtos")]
        public int TotalProdutos { get; set; }
    }
}


