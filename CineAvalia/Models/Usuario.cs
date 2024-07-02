using CineAvalia.Helper;
using System.ComponentModel.DataAnnotations;

namespace CineAvalia.Models
{
    public class Usuario
    {
        public enum Sexo
        {
            Masculino,
            Feminino,
            Outro
        }
        public enum Tipo
        {
            Administrador,
            Padrão
        }

        [Key]
        [Required(ErrorMessage = "Informação obrigatória")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.")]
        public string Senha { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }

        [Display(Name = "Genero")]
        public Sexo SexoUsuario { get; set; }

        [Display(Name = "Tipo")]
        public Tipo TipoUsuario { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

    }
}
