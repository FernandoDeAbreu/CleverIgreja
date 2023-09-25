using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleverIgreja.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CleverIgreja.Controllers
{
    public class MoedaController : Controller
    {
        private readonly BdSystemContext _context;

        public MoedaController(BdSystemContext context)
        {
            _context = context;
        }

        // GET: Moeda
        public async Task<IActionResult> Index()
        {
            return _context.Moeda != null ?
                        View(await _context.Moeda.ToListAsync()) :
                        Problem("Entity set 'BdSystemContext.Moeda'  is null.");
        }

        // GET: Moeda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moeda == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moeda
                .FirstOrDefaultAsync(m => m.MoedaId == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        // GET: Moeda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moeda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoedaId,Descricao")] Moeda moeda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moeda);
        }

        // GET: Moeda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moeda == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moeda.FindAsync(id);
            if (moeda == null)
            {
                return NotFound();
            }
            return View(moeda);
        }

        // POST: Moeda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoedaId,Descricao")] Moeda moeda)
        {
            if (id != moeda.MoedaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moeda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoedaExists(moeda.MoedaId))
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
            return View(moeda);
        }

        // GET: Moeda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moeda == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moeda
                .FirstOrDefaultAsync(m => m.MoedaId == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        // POST: Moeda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moeda == null)
            {
                return Problem("Entity set 'BdSystemContext.Moeda'  is null.");
            }
            var moeda = await _context.Moeda.FindAsync(id);
            if (moeda != null)
            {
                _context.Moeda.Remove(moeda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoedaExists(int id)
        {
            return (_context.Moeda?.Any(e => e.MoedaId == id)).GetValueOrDefault();
        }
    }
}
