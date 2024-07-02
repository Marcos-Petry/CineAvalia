using CineAvalia.Models;

namespace CineAvalia.Interfaces
{
    public interface IUsuario
    {
        Usuario BuscarPorEmail(string email);
    }
}
