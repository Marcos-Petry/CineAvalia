using System.ComponentModel.DataAnnotations;

namespace CineAvalia.Models
{
    public class Genero
    {
        [Key]
        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid Id { get; set; }

        [MaxLength(75, ErrorMessage = "Tamanho de string exedido")]
        [Required(ErrorMessage = "Informação obrigatória")]
        public string Nome { get; set; }

        // Propriedade de navegação reversa
       // public ICollection<Filme> Filmes { get; set; }
    }
}
