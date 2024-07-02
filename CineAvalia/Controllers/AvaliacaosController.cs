using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineAvalia.Data;
using CineAvalia.Models;
using CineAvalia.Filters;
using Newtonsoft.Json;

namespace CineAvalia.Controllers
{
    [PaginaUsuarioLogado]
    public class AvaliacaosController : Controller
    {
        private readonly CineAvaliaContext _context;

        public AvaliacaosController(CineAvaliaContext context)
        {
            _context = context;
        }

        // GET: Avaliacaos
        public async Task<IActionResult> Index()
        {
            var cineAvaliaContext = _context.Avaliacao.Include(a => a.Filme).Include(a => a.Usuario);
            return View(await cineAvaliaContext.ToListAsync());
        }

        // GET: Avaliacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao
                .Include(a => a.Filme)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacaos/Create
        public IActionResult Create()
        {
            ViewData["FilmeId"] = new SelectList(_context.Filme, "Id", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email");
            return View();
        }

        // POST: Avaliacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid Id,[Bind("Id,Nota,Comentario,FilmeId")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                avaliacao.Id = Guid.NewGuid();
                var jsonUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
                var usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
                avaliacao.UsuarioId = usuario.Id;
                avaliacao.FilmeId = Id;
                avaliacao.DataAvaliacao = DateTime.UtcNow;

                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeId"] = new SelectList(_context.Filme, "Id", "Descricao", avaliacao.FilmeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", avaliacao.UsuarioId);
            return View(avaliacao);
        }

        // GET: Avaliacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            ViewData["FilmeId"] = new SelectList(_context.Filme, "Id", "Descricao", avaliacao.FilmeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", avaliacao.UsuarioId);
            return View(avaliacao);
        }

        // POST: Avaliacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nota,Comentario,FilmeId")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var jsonUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
                    var usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
                    avaliacao.UsuarioId = usuario.Id;

                    avaliacao.DataAvaliacao = DateTime.UtcNow;
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeId"] = new SelectList(_context.Filme, "Id", "Descricao", avaliacao.FilmeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email", avaliacao.UsuarioId);
            return View(avaliacao);
        }

        // GET: Avaliacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao
                .Include(a => a.Filme)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacao.Remove(avaliacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(Guid id)
        {
            return _context.Avaliacao.Any(e => e.Id == id);
        }
    }
}
