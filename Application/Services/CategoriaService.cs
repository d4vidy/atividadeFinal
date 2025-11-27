using APS2.Application.Interfaces;
using APS2.Application.ViewModels;
using APS2.Domain.Entities;
using Mapster;

namespace APS2.Application.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterTodosAsync()
        {
            var categorias = await _repository.ObterTodosAsync();
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }

        public async Task<CategoriaViewModel?> ObterPorIdAsync(int id)
        {
            var categoria = await _repository.ObterPorIdAsync(id);
            return categoria?.Adapt<CategoriaViewModel>();
        }

        public async Task<IEnumerable<CategoriaViewModel>> BuscarPorNomeAsync(string nome)
        {
            var categorias = await _repository.BuscarPorNomeAsync(nome);
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }

        public async Task<CategoriaViewModel> CriarAsync(CategoriaViewModel viewModel)
        {
            var categoria = new Categoria(viewModel.Nome);
            await _repository.AdicionarAsync(categoria);
            return categoria.Adapt<CategoriaViewModel>();
        }

        public async Task<CategoriaViewModel?> AtualizarAsync(int id, CategoriaViewModel viewModel)
        {
            var categoria = await _repository.ObterPorIdAsync(id);
            if (categoria == null)
                return null;

            categoria.AtualizarNome(viewModel.Nome);
            await _repository.AtualizarAsync(categoria);
            return categoria.Adapt<CategoriaViewModel>();
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
