using Microsoft.EntityFrameworkCore;
using SistemaCatalogo.Domain.Entities;

namespace SistemaCatalogo.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor obrigatório para injeção de dependência e migrações
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para mapear as entidades para tabelas no banco de dados
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        // Aplica as configurações de entidades (como o relacionamento 1:N)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}