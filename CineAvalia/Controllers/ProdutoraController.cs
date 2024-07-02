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
    [PaginaRestritaAdmin]
    public class ProdutoraController : Controller
    {
        private readonly CineAvaliaContext _context;

        public ProdutoraController(CineAvaliaContext context)
        {
            _context = context;
        }

        // GET: Produtora
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtora.ToListAsync());
        }

        // GET: Produtora/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // GET: Produtora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Nacionalidade")] Produtora produtora)
        {
            if (ModelState.IsValid)
            {
                produtora.Id = Guid.NewGuid();
                _context.Add(produtora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtora);
        }

        // GET: Produtora/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtora.FindAsync(id);
            if (produtora == null)
            {
                return NotFound();
            }
            return View(produtora);
        }

        // POST: Produtora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Nacionalidade")] Produtora produtora)
        {
            if (id != produtora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoraExists(produtora.Id))
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
            return View(produtora);
        }

        // GET: Produtora/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // POST: Produtora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtora = await _context.Produtora.FindAsync(id);
            if (produtora != null)
            {
                _context.Produtora.Remove(produtora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoraExists(Guid id)
        {
            return _context.Produtora.Any(e => e.Id == id);
        }
    }
}
