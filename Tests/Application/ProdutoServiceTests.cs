using Moq;
using Xunit;
using APS2.Application.Interfaces;
using APS2.Application.Services;
using APS2.Application.ViewModels;
using APS2.Domain.Entities;

namespace Tests.Application
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task ProdutoService_ObterTodos_DeveRetornarLista()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            var produtos = new List<Produto>
            {
                new Produto("Notebook", 3500m, 1),
                new Produto("Mouse", 50m, 1)
            };
            mockProdutoRepository.Setup(r => r.ObterTodosAsync()).ReturnsAsync(produtos);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);

            // Act
            var resultado = await service.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            mockProdutoRepository.Verify(r => r.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ProdutoService_CriarProdutoComCategoriaValida_DeveRetornarViewModel()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            mockCategoriaRepository.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);
            mockProdutoRepository.Setup(r => r.AdicionarAsync(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);
            var viewModel = new ProdutoViewModel { Nome = "Notebook", Preco = 3500m, CategoriaId = 1 };

            // Act
            var resultado = await service.CriarAsync(viewModel);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Notebook", resultado.Nome);
            Assert.Equal(3500m, resultado.Preco);
            mockProdutoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task ProdutoService_CriarProdutoComCategoriaInvalida_DeveLançarException()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            mockCategoriaRepository.Setup(r => r.ExisteAsync(999)).ReturnsAsync(false);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);
            var viewModel = new ProdutoViewModel { Nome = "Notebook", Preco = 3500m, CategoriaId = 999 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.CriarAsync(viewModel));
        }

        [Fact]
        public async Task ProdutoService_AtualizarProduto_DeveRetornarViewModelAtualizado()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            var produto = new Produto("Notebook", 3500m, 1);
            mockProdutoRepository.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(produto);
            mockCategoriaRepository.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);
            mockProdutoRepository.Setup(r => r.AtualizarAsync(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);
            var viewModel = new ProdutoViewModel { Id = 1, Nome = "Notebook Gamer", Preco = 4500m, CategoriaId = 1 };

            // Act
            var resultado = await service.AtualizarAsync(1, viewModel);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Notebook Gamer", resultado.Nome);
            Assert.Equal(4500m, resultado.Preco);
        }

        [Fact]
        public async Task ProdutoService_RemoverProduto_DeveRetornarTrue()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            mockProdutoRepository.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);
            mockProdutoRepository.Setup(r => r.RemoverAsync(1)).Returns(Task.CompletedTask);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);

            // Act
            var resultado = await service.RemoverAsync(1);

            // Assert
            Assert.True(resultado);
            mockProdutoRepository.Verify(r => r.RemoverAsync(1), Times.Once);
        }

        [Fact]
        public async Task ProdutoService_ObterPorCategoria_DeveRetornarProdutosDaCategoria()
        {
            // Arrange
            var mockProdutoRepository = new Mock<IProdutoRepository>();
            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            var produtos = new List<Produto>
            {
                new Produto("Notebook", 3500m, 1),
                new Produto("Mouse", 50m, 1)
            };
            mockProdutoRepository.Setup(r => r.ObterPorCategoriaAsync(1)).ReturnsAsync(produtos);
            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);

            // Act
            var resultado = await service.ObterPorCategoriaAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }
    }
}
