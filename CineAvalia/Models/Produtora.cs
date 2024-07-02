using System.ComponentModel.DataAnnotations;

namespace CineAvalia.Models
{
    public class Produtora
    {
        [Key]
        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid Id { get; set; }

        [MaxLength(100, ErrorMessage = "Tamanho de string exedido")]
        [Required(ErrorMessage = "Informação obrigatória")]
        public string Nome { get; set; }

        [MaxLength(50, ErrorMessage = "Tamanho de string exedido")]
        [Required(ErrorMessage = "Informação obrigatória")]
        public string Nacionalidade { get; set; }
    }
}
