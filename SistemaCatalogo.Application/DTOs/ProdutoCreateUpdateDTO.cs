using System.ComponentModel.DataAnnotations;
using SistemaCatalogo.Application.Validation;

namespace SistemaCatalogo.Application.DTOs
{
    public class ProdutoCreateUpdateDTO
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome do produto deve ter no máximo 200 caracteres.")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; } = string.Empty;

        [PrecoMinimo(1.0)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [CategoriaExistente]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}


