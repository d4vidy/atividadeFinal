using APS2.Application.Interfaces;
using APS2.Domain.Entities;
using APS2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APS2.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoriaAsync(int categoriaId)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Where(p => p.CategoriaId == categoriaId)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Where(p => p.Nome.Contains(nome))
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Id == id);
        }
    }
}
