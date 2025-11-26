using Microsoft.Extensions.DependencyInjection;
using SistemaCatalogo.Application.Services;

namespace SistemaCatalogo.Application;

public static class ApplicationServiceRegistration
{
    // Método de extensão para registrar os serviços da camada de Aplicação
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Requisito 8: Registrar serviços de aplicação na DI
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IProdutoService, ProdutoService>();

        return services;
    }
}


