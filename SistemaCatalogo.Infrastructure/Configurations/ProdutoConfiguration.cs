using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Infrastructure.Configurations
{
    // Define explicitamente o mapeamento de Produto no banco de dados.
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Chave Primária
            builder.HasKey(p => p.Id);

            // Outras propriedades com restrições
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Preco).HasPrecision(10, 2);

            // Configuração Explícita do Relacionamento 1:N (Categoria 1 -> Produto N)
            builder.HasOne(p => p.Categoria)          // Um Produto tem UMA Categoria
                   .WithMany(c => c.Produtos)         // Uma Categoria tem MÚLTIPLOS Produtos
                   .HasForeignKey(p => p.CategoriaId) // Define CategoriaId como a Chave Estrangeira (FK)
                   .IsRequired();
        }
    }
}