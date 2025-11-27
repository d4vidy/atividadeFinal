using System.ComponentModel.DataAnnotations;
using APS2.Application.Validations;

namespace APS2.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        [NomeUnico]
        public string Nome { get; set; } = null!;
    }
}
