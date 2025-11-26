using Microsoft.Extensions.DependencyInjection;
using SistemaCatalogo.Application.Interfaces;
using SistemaCatalogo.Infrastructure.Repositories;

namespace SistemaCatalogo.Infrastructure.ServiceExtensions
{
    // Classe de extensão para registrar todos os serviços da camada Infrastructure na DI
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Requisito 8: Todos os serviços e repositórios devem ser registrados via DI.
            
            // Registra os repositórios com o padrão de Injeção de Dependência
            // Os repositórios devem ser Singletons ou Scoped. Scoped é mais comum para DbContext.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            // Registra os repositórios específicos (eles usam a implementação genérica)
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}