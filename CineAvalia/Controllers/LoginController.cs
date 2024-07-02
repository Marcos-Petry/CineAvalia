using CineAvalia.Models;
using Microsoft.AspNetCore.Mvc;
using CineAvalia.Implementacoes;
using CineAvalia.Helper;
namespace CineAvalia.Controllers
{
    public class LoginController : Controller
    {

        private readonly UsuarioImplementacao _usuarioImplementacao;
        private readonly ISessao _sessao;
        public LoginController(UsuarioImplementacao usuarioImplementacao, ISessao sessao)
        {
            _usuarioImplementacao = usuarioImplementacao;
            _sessao               = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public IActionResult Logar(LoginModel LoginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuarioImplementacao.BuscarPorEmail(LoginModel.Email);

                    if (usuario != null && usuario.SenhaValida(LoginModel.Senha))
                    {
                        _sessao.CriarSessaoUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    { 
                        TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s), Tente novamente.";
                    }

                }

                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
