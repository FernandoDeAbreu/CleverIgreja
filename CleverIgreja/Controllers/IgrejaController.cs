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
    public class IgrejaController : Controller
    {
        private readonly BdSystemContext _context;

        public IgrejaController(BdSystemContext context)
        {
            _context = context;
        }

        // GET: Igreja
        public async Task<IActionResult> Index()
        {
            return _context.Igreja != null ?
                        View(await _context.Igreja.ToListAsync()) :
                        Problem("Entity set 'BdSystemContext.Igreja'  is null.");
        }

        // GET: Igreja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.Igreja
                .FirstOrDefaultAsync(m => m.IgrejaId == id);
            if (igreja == null)
            {
                return NotFound();
            }

            return View(igreja);
        }

        // GET: Igreja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igreja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IgrejaId,Nome,TipoPessoa,DataNascimento,CnpjCpf,RgIe,Cep,Cidade,UF,Bairro,Endereco,Numero,Complemento,Celular,Whatsapp,Telefone,Email,Observacao")] Igreja igreja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igreja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igreja);
        }

        // GET: Igreja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.Igreja.FindAsync(id);
            if (igreja == null)
            {
                return NotFound();
            }
            return View(igreja);
        }

        // POST: Igreja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IgrejaId,Nome,TipoPessoa,DataNascimento,CnpjCpf,RgIe,Cep,Cidade,UF,Bairro,Endereco,Numero,Complemento,Celular,Whatsapp,Telefone,Email,Observacao")] Igreja igreja)
        {
            if (id != igreja.IgrejaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igreja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgrejaExists(igreja.IgrejaId))
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
            return View(igreja);
        }

        // GET: Igreja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.Igreja
                .FirstOrDefaultAsync(m => m.IgrejaId == id);
            if (igreja == null)
            {
                return NotFound();
            }

            return View(igreja);
        }

        // POST: Igreja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Igreja == null)
            {
                return Problem("Entity set 'BdSystemContext.Igreja'  is null.");
            }
            var igreja = await _context.Igreja.FindAsync(id);
            if (igreja != null)
            {
                _context.Igreja.Remove(igreja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgrejaExists(int id)
        {
            return (_context.Igreja?.Any(e => e.IgrejaId == id)).GetValueOrDefault();
        }
    }
}
