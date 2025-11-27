using APS2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APS2.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Aplicar migrations pendentes
            await context.Database.MigrateAsync();

            // Verificar se há dados
            if (await context.Categorias.AnyAsync())
                return; // Dados já existem

            // Criar categorias
            var categorias = new List<Categoria>
            {
                new Categoria("Eletrônicos"),
                new Categoria("Livros"),
                new Categoria("Roupas"),
                new Categoria("Alimentos"),
                new Categoria("Móveis")
            };

            await context.Categorias.AddRangeAsync(categorias);
            await context.SaveChangesAsync();

            // Recarregar categorias para obter IDs
            var categoriasDb = await context.Categorias.ToListAsync();

            // Criar produtos
            var produtos = new List<Produto>
            {
                new Produto("Notebook Dell", 3500.00m, categoriasDb[0].Id),
                new Produto("Mouse Logitech", 150.00m, categoriasDb[0].Id),
                new Produto("Teclado Mecânico", 450.00m, categoriasDb[0].Id),
                new Produto("C# Programming", 89.90m, categoriasDb[1].Id),
                new Produto("Clean Code", 120.00m, categoriasDb[1].Id),
                new Produto("Camiseta Básica", 49.90m, categoriasDb[2].Id),
                new Produto("Calça Jeans", 129.90m, categoriasDb[2].Id),
                new Produto("Arroz Integral", 12.50m, categoriasDb[3].Id),
                new Produto("Feijão Carioca", 8.90m, categoriasDb[3].Id),
                new Produto("Cadeira Gamer", 799.90m, categoriasDb[4].Id),
                new Produto("Mesa de Escritório", 599.90m, categoriasDb[4].Id)
            };

            await context.Produtos.AddRangeAsync(produtos);
            await context.SaveChangesAsync();
        }
    }
}
