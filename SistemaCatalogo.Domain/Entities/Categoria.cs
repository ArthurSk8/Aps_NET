using System.Collections.Generic;

namespace SistemaCatalogo.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        // Coleção de navegação para o lado 'Muitos'
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}