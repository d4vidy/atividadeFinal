using APS2.Domain.Entities;
using APS2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APS2.Infrastructure.Seed
{
    public static class DataSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Apenas seed se o banco estiver vazio
            if (await context.Categorias.AnyAsync())
                return;

            // Criar categorias
            var categorias = new List<Categoria>
            {
                new Categoria("Eletrônicos"),
                new Categoria("Livros"),
                new Categoria("Alimentos"),
                new Categoria("Vestuário"),
                new Categoria("Esportes")
            };

            await context.Categorias.AddRangeAsync(categorias);
            await context.SaveChangesAsync();

            // Criar produtos
            var produtos = new List<Produto>
            {
                new Produto("Notebook Dell", 3500.00m, 1),
                new Produto("Mouse Logitech", 89.90m, 1),
                new Produto("Teclado Mecânico", 450.00m, 1),
                new Produto("Clean Code", 89.50m, 2),
                new Produto("DDD in Practice", 120.00m, 2),
                new Produto("Arroz Integral 5kg", 25.99m, 3),
                new Produto("Feijão Carioca 1kg", 8.50m, 3),
                new Produto("Camiseta Básica", 45.00m, 4),
                new Produto("Calça Jeans", 120.00m, 4),
                new Produto("Bola de Futebol", 89.90m, 5),
                new Produto("Bola de Vôlei", 99.90m, 5)
            };

            await context.Produtos.AddRangeAsync(produtos);
            await context.SaveChangesAsync();
        }
    }
}
