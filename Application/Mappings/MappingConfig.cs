using APS2.Application.ViewModels;
using APS2.Domain.Entities;
using Mapster;

namespace APS2.Application.Mappings
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            // Categoria mapping
            TypeAdapterConfig<Categoria, CategoriaViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nome, src => src.Nome);

            TypeAdapterConfig<CategoriaViewModel, Categoria>.NewConfig()
                .ConstructUsing(src => new Categoria(src.Nome))
                .Ignore(dest => dest.Produtos);

            // Produto mapping
            TypeAdapterConfig<Produto, ProdutoViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nome, src => src.Nome)
                .Map(dest => dest.Preco, src => src.Preco)
                .Map(dest => dest.CategoriaId, src => src.CategoriaId);

            TypeAdapterConfig<ProdutoViewModel, Produto>.NewConfig()
                .ConstructUsing(src => new Produto(src.Nome, src.Preco, src.CategoriaId));
        }
    }
}
