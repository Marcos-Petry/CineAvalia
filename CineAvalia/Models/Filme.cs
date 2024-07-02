using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineAvalia.Models
{
    public class Filme
    {
        [Key]
        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public int AnoLancamento { get; set; }

        [MaxLength(300, ErrorMessage = "Tamanho de string exedido")]
        [Required(ErrorMessage = "Informação obrigatória")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid GeneroId { get; set; }

        // usado como relacionamento do entity framework
        public Genero Genero { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid ProdutoraId { get; set; }
            
        public Produtora Produtora { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }

        public byte[] Imagem { get; set; }

        [NotMapped]  // Esta propriedade não será mapeada no banco de dados
        public IFormFile ImagemFile { get; set; }  // Propriedade para o upload da imagem



        /*
         precisa criar a migration, e depois fazer o update dela pra criar. aoi fazer isso ele cria a base e as tabelsa
         */

    }
}
