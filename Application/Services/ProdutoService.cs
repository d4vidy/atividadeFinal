using APS2.Application.Interfaces;
using APS2.Application.ViewModels;
using APS2.Domain.Entities;
using Mapster;

namespace APS2.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoService(IProdutoRepository repository, ICategoriaRepository categoriaRepository)
        {
            _repository = repository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodosAsync()
        {
            var produtos = await _repository.ObterTodosAsync();
            return produtos.Adapt<IEnumerable<ProdutoViewModel>>();
        }

        public async Task<ProdutoViewModel?> ObterPorIdAsync(int id)
        {
            var produto = await _repository.ObterPorIdAsync(id);
            return produto?.Adapt<ProdutoViewModel>();
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoriaAsync(int categoriaId)
        {
            var produtos = await _repository.ObterPorCategoriaAsync(categoriaId);
            return produtos.Adapt<IEnumerable<ProdutoViewModel>>();
        }

        public async Task<IEnumerable<ProdutoViewModel>> BuscarPorNomeAsync(string nome)
        {
            var produtos = await _repository.BuscarPorNomeAsync(nome);
            return produtos.Adapt<IEnumerable<ProdutoViewModel>>();
        }

        public async Task<ProdutoViewModel> CriarAsync(ProdutoViewModel viewModel)
        {
            // Validar se categoria existe
            var categoriaExiste = await _categoriaRepository.ExisteAsync(viewModel.CategoriaId);
            if (!categoriaExiste)
                throw new ArgumentException($"Categoria com ID {viewModel.CategoriaId} não existe.");

            var produto = new Produto(viewModel.Nome, viewModel.Preco, viewModel.CategoriaId);
            await _repository.AdicionarAsync(produto);
            return produto.Adapt<ProdutoViewModel>();
        }

        public async Task<ProdutoViewModel?> AtualizarAsync(int id, ProdutoViewModel viewModel)
        {
            var produto = await _repository.ObterPorIdAsync(id);
            if (produto == null)
                return null;

            // Validar se categoria existe (se foi alterada)
            if (produto.CategoriaId != viewModel.CategoriaId)
            {
                var categoriaExiste = await _categoriaRepository.ExisteAsync(viewModel.CategoriaId);
                if (!categoriaExiste)
                    throw new ArgumentException($"Categoria com ID {viewModel.CategoriaId} não existe.");
            }

            produto.AtualizarNome(viewModel.Nome);
            produto.AtualizarPreco(viewModel.Preco);

            await _repository.AtualizarAsync(produto);
            return produto.Adapt<ProdutoViewModel>();
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var existe = await _repository.ExisteAsync(id);
            if (!existe)
                return false;

            await _repository.RemoverAsync(id);
            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _repository.ExisteAsync(id);
        }
    }
}
