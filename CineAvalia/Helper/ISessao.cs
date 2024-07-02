using CineAvalia.Models;

namespace CineAvalia.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(Usuario usuario);

        void RemoverSessaoUsuario();

        Usuario BuscarSessaoUsuario();
    }
}
