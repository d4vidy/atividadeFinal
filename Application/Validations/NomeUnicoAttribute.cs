using System.ComponentModel.DataAnnotations;

namespace APS2.Application.Validations
{
    /// <summary>
    /// Custom validation attribute que verifica se o nome é único.
    /// Este atributo é placeholder e deve ser injetado com contexto real em controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NomeUnicoAttribute : ValidationAttribute
    {
        private readonly Type? _contextType;

        public NomeUnicoAttribute(Type? contextType = null)
        {
            _contextType = contextType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            var nome = value.ToString() ?? string.Empty;

            // Placeholder validation logic
            // Em um cenário real, você teria acesso ao DbContext ou repositório via DI
            // e verificaria se o nome já existe na base de dados
            
            // Exemplo de regra simples: nome não pode conter números consecutivos
            if (nome.Contains("00") || nome.Contains("11") || nome.Contains("22") || 
                nome.Contains("33") || nome.Contains("44") || nome.Contains("55") || 
                nome.Contains("66") || nome.Contains("77") || nome.Contains("88") || 
                nome.Contains("99"))
            {
                return new ValidationResult($"O nome '{nome}' não pode conter números consecutivos iguais.");
            }

            return ValidationResult.Success;
        }
    }
}
