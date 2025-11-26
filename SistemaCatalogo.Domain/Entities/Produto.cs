namespace SistemaCatalogo.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;

        // Chave Estrangeira explícita (Foreign Key)
        public int CategoriaId { get; set; }

        // Referência de navegação para o lado 'Um'
        public Categoria Categoria { get; set; }
    }
}