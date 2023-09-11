using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CleverIgreja.Models;

namespace CleverIgreja.Controllers
{
    public class MembroController : Controller
    {
        private readonly BdSystemContext _context;

        public MembroController(BdSystemContext context)
        {
            _context = context;
        }

        // GET: Membro
        public async Task<IActionResult> Index()
        {
              return View(await _context.Membro.ToListAsync());
        }

        // GET: Membro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membro == null)
            {
                return NotFound();
            }

            var membro = await _context.Membro
                .FirstOrDefaultAsync(m => m.MembroId == id);
            if (membro == null)
            {
                return NotFound();
            }

            return View(membro);
        }

        // GET: Membro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembroId,Nome,TipoPessoa,DataNascimento,CnpjCpf,RgIe,Cep,Cidade,UF,Bairro,Endereco,Numero,Complemento,Celular,Whatsapp,Telefone,Email,Observacao")] Membro membro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membro);
        }

        // GET: Membro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membro == null)
            {
                return NotFound();
            }

            var membro = await _context.Membro.FindAsync(id);
            if (membro == null)
            {
                return NotFound();
            }
            return View(membro);
        }

        // POST: Membro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembroId,Nome,TipoPessoa,DataNascimento,CnpjCpf,RgIe,Cep,Cidade,UF,Bairro,Endereco,Numero,Complemento,Celular,Whatsapp,Telefone,Email,Observacao")] Membro membro)
        {
            if (id != membro.MembroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembroExists(membro.MembroId))
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
            return View(membro);
        }

        // GET: Membro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membro == null)
            {
                return NotFound();
            }

            var membro = await _context.Membro
                .FirstOrDefaultAsync(m => m.MembroId == id);
            if (membro == null)
            {
                return NotFound();
            }

            return View(membro);
        }

        // POST: Membro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membro == null)
            {
                return Problem("Entity set 'BdSystemContext.Membro'  is null.");
            }
            var membro = await _context.Membro.FindAsync(id);
            if (membro != null)
            {
                _context.Membro.Remove(membro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembroExists(int id)
        {
          return _context.Membro.Any(e => e.MembroId == id);
        }
    }
}
