using System.ComponentModel.DataAnnotations;

namespace SistemaCatalogo.Application.Validation
{
    // Requisito 6: Validação Personalizada 1
    // Garante que o valor decimal (Preco) seja maior ou igual a um valor mínimo.
    public class PrecoMinimoAttribute : ValidationAttribute
    {
        private readonly decimal _precoMinimo;

        public PrecoMinimoAttribute(double precoMinimo)
        {
            // Converte o valor de double para decimal para precisão
            _precoMinimo = (decimal)precoMinimo;
            
            // Define a mensagem de erro padrão
            ErrorMessage = $"O preço deve ser de R$ {_precoMinimo:N2} ou superior.";
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal preco)
            {
                if (preco < _precoMinimo)
                {
                    // Retorna a falha na validação com a mensagem de erro
                    return new ValidationResult(ErrorMessage);
                }
            }
            
            // Se o valor for nulo, ou não for decimal, ou for válido, retorna sucesso
            return ValidationResult.Success;
        }
    }
}