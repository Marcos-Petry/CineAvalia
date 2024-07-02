using System.ComponentModel.DataAnnotations;

namespace CineAvalia.Models
{
    public class Avaliacao
    {
        [Key]
        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public int Nota { get; set; }

        [MaxLength(200, ErrorMessage = "Tamanho de string exedido")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid FilmeId { get; set; }

        public Filme Filme { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
