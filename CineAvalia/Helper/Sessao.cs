using CineAvalia.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CineAvalia.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _htttpContext;

        public Sessao(IHttpContextAccessor htttpContext)
        {
            _htttpContext = htttpContext;
        }

        public Usuario BuscarSessaoUsuario()
        {
            string sessaoUsuario = _htttpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
        }

        public void CriarSessaoUsuario(Usuario usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _htttpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _htttpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
