using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APS2.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; private set; }
        
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; private set; } = null!;

        public ICollection<Produto> Produtos { get; private set; } = new List<Produto>();

        public Categoria() { }

        public Categoria(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        public void AtualizarNome(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da categoria não pode ser vazio.", nameof(nome));
            
            if (nome.Length < 3 || nome.Length > 150)
                throw new ArgumentException("Nome deve ter entre 3 e 150 caracteres.", nameof(nome));
        }
    }
}
