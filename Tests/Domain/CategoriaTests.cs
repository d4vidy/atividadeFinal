using Xunit;
using APS2.Domain.Entities;

namespace Tests.Domain
{
    public class CategoriaTests
    {
        [Fact]
        public void Categoria_CriarComNomeValido_DeveRetornarObjeto()
        {
            // Arrange
            var nome = "Eletrônicos";

            // Act
            var categoria = new Categoria(nome);

            // Assert
            Assert.NotNull(categoria);
            Assert.Equal(nome, categoria.Nome);
        }

        [Fact]
        public void Categoria_CriarComNomeVazio_DeveLançarException()
        {
            // Arrange
            var nome = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Categoria(nome));
        }

        [Fact]
        public void Categoria_CriarComNomeMuitoCurto_DeveLançarException()
        {
            // Arrange
            var nome = "AB"; // menos de 3 caracteres

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Categoria(nome));
        }

        [Fact]
        public void Categoria_CriarComNomeMuitoLongo_DeveLançarException()
        {
            // Arrange
            var nome = new string('A', 151); // mais de 150 caracteres

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Categoria(nome));
        }

        [Fact]
        public void Categoria_AtualizarNomeValido_DeveAtualizarSucesso()
        {
            // Arrange
            var categoria = new Categoria("Eletrônicos");
            var novoNome = "Informática";

            // Act
            categoria.AtualizarNome(novoNome);

            // Assert
            Assert.Equal(novoNome, categoria.Nome);
        }

        [Fact]
        public void Categoria_AtualizarParaNomeInvalido_DeveLançarException()
        {
            // Arrange
            var categoria = new Categoria("Eletrônicos");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => categoria.AtualizarNome(""));
        }
    }
}
