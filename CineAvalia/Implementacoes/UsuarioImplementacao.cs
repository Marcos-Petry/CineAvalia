using CineAvalia.Data;
using CineAvalia.Models;

namespace CineAvalia.Implementacoes
{
    public class UsuarioImplementacao
    {

        private readonly CineAvaliaContext  _context;

        public UsuarioImplementacao(CineAvaliaContext bancoContext)
        {
            this._context = bancoContext;
        }

        public Usuario BuscarPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(u => u.Email == email);
        }
    }
}
