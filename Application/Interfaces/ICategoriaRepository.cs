using APS2.Domain.Entities;

namespace APS2.Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterTodosAsync();
        Task<Categoria?> ObterPorIdAsync(int id);
        Task<Categoria?> ObterComProdutosAsync(int id);
        Task<IEnumerable<Categoria>> BuscarPorNomeAsync(string nome);
        Task AdicionarAsync(Categoria categoria);
        Task AtualizarAsync(Categoria categoria);
        Task RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
