using APS2.Domain.Entities;

namespace APS2.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
