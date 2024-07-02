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

namespace CineAvalia.Controllers
{
    [PaginaUsuarioLogado]
    public class FilmeController : Controller
    {
        private readonly CineAvaliaContext _context;

        public FilmeController(CineAvaliaContext context)
        {
            _context = context;
        }

        // GET: Filme
        public async Task<IActionResult> Index()
        {
            var cineAvaliaContext = _context.Filme.Include(f => f.Genero).Include(f => f.Produtora);
            return View(await cineAvaliaContext.ToListAsync());
        }

        // GET: Filme/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Genero)
                .Include(f => f.Produtora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filme/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nome");
            ViewData["ProdutoraId"] = new SelectList(_context.Produtora, "Id", "Nacionalidade");
            return View();
        }

        // POST: Filme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,AnoLancamento,Descricao,GeneroId,ProdutoraId, ImagemFile")] Filme filme)
        {
            if (ModelState.IsValid)
            {

                if (filme.ImagemFile != null && filme.ImagemFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await filme.ImagemFile.CopyToAsync(memoryStream);
                        filme.Imagem = memoryStream.ToArray();
                    }
                }


                filme.Id = Guid.NewGuid();
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nome", filme.GeneroId);
            ViewData["ProdutoraId"] = new SelectList(_context.Produtora, "Id", "Nacionalidade", filme.ProdutoraId);
            return View(filme);
        }

        // GET: Filme/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nome", filme.GeneroId);
            ViewData["ProdutoraId"] = new SelectList(_context.Produtora, "Id", "Nacionalidade", filme.ProdutoraId);
            return View(filme);
        }

        // POST: Filme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,AnoLancamento,Descricao,GeneroId,ProdutoraId")] Filme filme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.Id))
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
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nome", filme.GeneroId);
            ViewData["ProdutoraId"] = new SelectList(_context.Produtora, "Id", "Nacionalidade", filme.ProdutoraId);
            return View(filme);
        }

        // GET: Filme/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Genero)
                .Include(f => f.Produtora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var filme = await _context.Filme.FindAsync(id);
            if (filme != null)
            {
                _context.Filme.Remove(filme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(Guid id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
    }
}
