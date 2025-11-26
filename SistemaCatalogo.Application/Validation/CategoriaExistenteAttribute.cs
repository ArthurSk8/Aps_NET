using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using SistemaCatalogo.Application.Interfaces;

namespace SistemaCatalogo.Application.Validation
{
    // Requisito 6: Validação Personalizada 2
    // Garante que o CategoriaId referenciado no DTO realmente exista no banco.
    public class CategoriaExistenteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int categoriaId)
            {
                // Verifica se o ID é zero ou negativo, que já seria inválido
                if (categoriaId <= 0)
                {
                    return new ValidationResult("O ID da Categoria não é válido.");
                }

                // Obtém o serviço de repositório via Injeção de Dependência (DI)
                // Usamos o ServiceProvider para resolver a dependência do repositório
                var categoriaRepository = validationContext.GetService<ICategoriaRepository>();
                
                // Busca a categoria de forma síncrona (Necessário, pois ValidationAttributes são síncronos)
                // NOTA: Em um projeto real, essa busca seria otimizada para ser assíncrona
                // ou a validação seria movida para uma camada de regras de negócio assíncrona.
                var categoria = categoriaRepository.GetByIdAsync(categoriaId).GetAwaiter().GetResult();

                if (categoria == null)
                {
                    // Falha na validação
                    return new ValidationResult("A Categoria selecionada não existe.");
                }
            }

            // O ID é válido ou não pôde ser convertido
            return ValidationResult.Success;
        }
    }
}