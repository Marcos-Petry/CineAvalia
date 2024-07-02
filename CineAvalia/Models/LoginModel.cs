using System.ComponentModel.DataAnnotations;

namespace CineAvalia.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Informação obrigatória")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informação obrigatória")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "A senha forenecida deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.")]
        public string Senha { get; set; }
    }
}
