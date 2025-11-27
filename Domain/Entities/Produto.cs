using System.ComponentModel.DataAnnotations;

namespace APS2.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres.")]
        public string Nome { get; private set; } = null!;
        
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; private set; }

        // Foreign key - obrigatória
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; private set; }
        public Categoria? Categoria { get; private set; }

        public Produto() { }

        public Produto(string nome, decimal preco, int categoriaId)
        {
            ValidarNome(nome);
            ValidarPreco(preco);
            
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId;
        }

        public void AtualizarPreco(decimal preco)
        {
            ValidarPreco(preco);
            Preco = preco;
        }

        public void AtualizarNome(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio.", nameof(nome));
            
            if (nome.Length < 3 || nome.Length > 200)
                throw new ArgumentException("Nome deve ter entre 3 e 200 caracteres.", nameof(nome));
        }

        private void ValidarPreco(decimal preco)
        {
            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.", nameof(preco));
        }
    }
}
