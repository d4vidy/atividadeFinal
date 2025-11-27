using Moq;
using Xunit;
using APS2.Application.Interfaces;
using APS2.Application.Services;
using APS2.Application.ViewModels;
using APS2.Domain.Entities;

namespace Tests.Application
{
    public class CategoriaServiceTests
    {
        [Fact]
        public async Task CategoriaService_ObterTodos_DeveRetornarLista()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var categorias = new List<Categoria>
            {
                new Categoria("Eletrônicos"),
                new Categoria("Livros")
            };
            mockRepository.Setup(r => r.ObterTodosAsync()).ReturnsAsync(categorias);
            var service = new CategoriaService(mockRepository.Object);

            // Act
            var resultado = await service.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            mockRepository.Verify(r => r.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task CategoriaService_ObterPorId_DeveRetornarCategoria()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var categoria = new Categoria("Eletrônicos");
            mockRepository.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(categoria);
            var service = new CategoriaService(mockRepository.Object);

            // Act
            var resultado = await service.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Eletrônicos", resultado.Nome);
        }

        [Fact]
        public async Task CategoriaService_CriarNovaCategoria_DeveRetornarViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.AdicionarAsync(It.IsAny<Categoria>())).Returns(Task.CompletedTask);
            var service = new CategoriaService(mockRepository.Object);
            var viewModel = new CategoriaViewModel { Nome = "Eletrônicos" };

            // Act
            var resultado = await service.CriarAsync(viewModel);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Eletrônicos", resultado.Nome);
            mockRepository.Verify(r => r.AdicionarAsync(It.IsAny<Categoria>()), Times.Once);
        }

        [Fact]
        public async Task CategoriaService_AtualizarCategoria_DeveRetornarViewModelAtualizado()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var categoria = new Categoria("Eletrônicos");
            mockRepository.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(categoria);
            mockRepository.Setup(r => r.AtualizarAsync(It.IsAny<Categoria>())).Returns(Task.CompletedTask);
            var service = new CategoriaService(mockRepository.Object);
            var viewModel = new CategoriaViewModel { Id = 1, Nome = "Informática" };

            // Act
            var resultado = await service.AtualizarAsync(1, viewModel);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Informática", resultado.Nome);
            mockRepository.Verify(r => r.AtualizarAsync(It.IsAny<Categoria>()), Times.Once);
        }

        [Fact]
        public async Task CategoriaService_RemoverCategoria_DeveRetornarTrue()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);
            mockRepository.Setup(r => r.RemoverAsync(1)).Returns(Task.CompletedTask);
            var service = new CategoriaService(mockRepository.Object);

            // Act
            var resultado = await service.RemoverAsync(1);

            // Assert
            Assert.True(resultado);
            mockRepository.Verify(r => r.RemoverAsync(1), Times.Once);
        }

        [Fact]
        public async Task CategoriaService_RemoverCategoriaInexistente_DeveRetornarFalse()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.ExisteAsync(999)).ReturnsAsync(false);
            var service = new CategoriaService(mockRepository.Object);

            // Act
            var resultado = await service.RemoverAsync(999);

            // Assert
            Assert.False(resultado);
            mockRepository.Verify(r => r.RemoverAsync(999), Times.Never);
        }
    }
}
