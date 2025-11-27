using System.ComponentModel.DataAnnotations;
using Xunit;
using APS2.Application.Validations;
using APS2.Application.ViewModels;

namespace Tests.Application
{
    public class ValidationAttributesTests
    {
        [Fact]
        public void NomeUnicoAttribute_ComNomeValido_DeveSerValido()
        {
            // Arrange
            var attribute = new NomeUnicoAttribute();
            var context = new ValidationContext(new CategoriaViewModel { Nome = "Eletrônicos" });
            var value = "Eletrônicos";

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void NomeUnicoAttribute_ComNúmerosConsecutivosIguais_DeveSerInvalido()
        {
            // Arrange
            var attribute = new NomeUnicoAttribute();
            var context = new ValidationContext(new CategoriaViewModel { Nome = "Categoria00" });
            var value = "Categoria00"; // contém "00" (números iguais consecutivos)

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.NotNull(result);
        }

        [Fact]
        public void NomeUnicoAttribute_ComValorNulo_DeveSerValido()
        {
            // Arrange
            var attribute = new NomeUnicoAttribute();
            var context = new ValidationContext(new CategoriaViewModel { Nome = "" });

            // Act
            var result = attribute.GetValidationResult(null, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void PrecoMaiorQueAttribute_ComPreçoValido_DeveSerValido()
        {
            // Arrange
            var attribute = new PrecoMaiorQueAttribute(0.01);
            var context = new ValidationContext(new ProdutoViewModel { Preco = 100.00m });
            var value = 100.00m;

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void PrecoMaiorQueAttribute_ComPreçoZero_DeveSerInvalido()
        {
            // Arrange
            var attribute = new PrecoMaiorQueAttribute(0.01);
            var context = new ValidationContext(new ProdutoViewModel { Preco = 0m });
            var value = 0m;

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.NotNull(result);
        }

        [Fact]
        public void PrecoMaiorQueAttribute_ComPreçoNegativo_DeveSerInvalido()
        {
            // Arrange
            var attribute = new PrecoMaiorQueAttribute(0.01);
            var context = new ValidationContext(new ProdutoViewModel { Preco = -50m });
            var value = -50m;

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.NotNull(result);
        }

        [Fact]
        public void PrecoMaiorQueAttribute_ComPreçoMuitoAlto_DeveSerInvalido()
        {
            // Arrange
            var attribute = new PrecoMaiorQueAttribute(0.01);
            var context = new ValidationContext(new ProdutoViewModel { Preco = 1000000m });
            var value = 1000000m; // supera 999999.99

            // Act
            var result = attribute.GetValidationResult(value, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.NotNull(result);
        }

        [Fact]
        public void PrecoMaiorQueAttribute_ComValorNulo_DeveSerValido()
        {
            // Arrange
            var attribute = new PrecoMaiorQueAttribute(0.01);
            var context = new ValidationContext(new ProdutoViewModel { Preco = 0m });

            // Act
            var result = attribute.GetValidationResult(null, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }
    }
}
