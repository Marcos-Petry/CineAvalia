﻿using CineAvalia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CineAvalia.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                Usuario usuarioPadrao = new Usuario();

                return View("Default", usuarioPadrao);
            }

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            return View("Default", usuario);
        }
    }
}
