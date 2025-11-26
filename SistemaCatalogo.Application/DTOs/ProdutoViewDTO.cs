using System.ComponentModel.DataAnnotations;

namespace SistemaCatalogo.Application.DTOs
{
    public class ProdutoViewDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        // Propriedade de conveniência para exibir o nome da categoria na View
        [Display(Name = "Categoria")]
        public string NomeCategoria { get; set; } = string.Empty;
    }
}


