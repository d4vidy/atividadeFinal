using Xunit;
using APS2.Domain.Entities;

namespace Tests.Domain
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_CriarComDadosValidos_DeveRetornarObjeto()
        {
            // Arrange
            var nome = "Notebook";
            var preco = 3500.00m;
            var categoriaId = 1;

            // Act
            var produto = new Produto(nome, preco, categoriaId);

            // Assert
            Assert.NotNull(produto);
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(preco, produto.Preco);
            Assert.Equal(categoriaId, produto.CategoriaId);
        }

        [Fact]
        public void Produto_CriarComNomeVazio_DeveLançarException()
        {
            // Arrange
            var nome = "";
            var preco = 100.00m;
            var categoriaId = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Produto(nome, preco, categoriaId));
        }

        [Fact]
        public void Produto_CriarComPreçoZero_DeveLançarException()
        {
            // Arrange
            var nome = "Notebook";
            var preco = 0m;
            var categoriaId = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Produto(nome, preco, categoriaId));
        }

        [Fact]
        public void Produto_CriarComPreçoNegativo_DeveLançarException()
        {
            // Arrange
            var nome = "Notebook";
            var preco = -100.00m;
            var categoriaId = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Produto(nome, preco, categoriaId));
        }

        [Fact]
        public void Produto_AtualizarPreçoValido_DeveAtualizarSucesso()
        {
            // Arrange
            var produto = new Produto("Notebook", 3500.00m, 1);
            var novoPreco = 3800.00m;

            // Act
            produto.AtualizarPreco(novoPreco);

            // Assert
            Assert.Equal(novoPreco, produto.Preco);
        }

        [Fact]
        public void Produto_AtualizarParaPreçoInvalido_DeveLançarException()
        {
            // Arrange
            var produto = new Produto("Notebook", 3500.00m, 1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => produto.AtualizarPreco(0m));
        }

        [Fact]
        public void Produto_AtualizarNomeValido_DeveAtualizarSucesso()
        {
            // Arrange
            var produto = new Produto("Notebook", 3500.00m, 1);
            var novoNome = "Notebook Gamer";

            // Act
            produto.AtualizarNome(novoNome);

            // Assert
            Assert.Equal(novoNome, produto.Nome);
        }

        [Fact]
        public void Produto_CriarComNomeMuitoLongo_DeveLançarException()
        {
            // Arrange
            var nome = new string('A', 201); // mais de 200 caracteres
            var preco = 100.00m;
            var categoriaId = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Produto(nome, preco, categoriaId));
        }
    }
}
