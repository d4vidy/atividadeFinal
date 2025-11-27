using System.ComponentModel.DataAnnotations;
using APS2.Application.Validations;

namespace APS2.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres.")]
        [NomeUnico]
        public string Nome { get; set; } = null!;
        
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 999.999,99.")]
        [PrecoMaiorQue(0.01)]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}
