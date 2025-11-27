using System.ComponentModel.DataAnnotations;

namespace APS2.Application.Validations
{
    /// <summary>
    /// Custom validation attribute que valida se um valor decimal é positivo e maior que um mínimo especificado.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrecoMaiorQueAttribute : ValidationAttribute
    {
        private readonly decimal _minValue;

        public PrecoMaiorQueAttribute(double minValue = 0.01)
        {
            _minValue = (decimal)minValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!decimal.TryParse(value.ToString(), out var preco))
                return new ValidationResult($"O valor '{value}' não é um preço válido.");

            if (preco <= _minValue)
                return new ValidationResult($"O preço deve ser maior que {_minValue:C}.");

            // Validação adicional: preço não deve exceder limite razoável
            if (preco > 999999.99m)
                return new ValidationResult($"O preço não pode exceder R$ 999.999,99.");

            return ValidationResult.Success;
        }
    }
}
