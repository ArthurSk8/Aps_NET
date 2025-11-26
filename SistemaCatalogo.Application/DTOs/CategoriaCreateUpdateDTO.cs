using System.ComponentModel.DataAnnotations;

namespace SistemaCatalogo.Application.DTOs
{
    public class CategoriaCreateUpdateDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; } = string.Empty;
    }
}


