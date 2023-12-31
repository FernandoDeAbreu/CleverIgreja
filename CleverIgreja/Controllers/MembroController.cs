﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CleverIgreja.Models;
using CleverIgreja.Shared;

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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescricaoParm"] = String.IsNullOrEmpty(sortOrder) ? "descricao_desc" : "";
            ViewData["ReferenciaParm"] = sortOrder == "Referencia" ? "referencia_desc" : "Referencia";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var membro = from s in _context.Membro
                                 select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                membro = membro.Where(s => s.Nome.Contains(searchString)
                                       || s.CnpjCpf.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "descricao_desc":
                    membro = membro.OrderByDescending(s => s.Nome);
                    break;
                case "Referencia":
                    membro = membro.OrderBy(s => s.DataNascimento);
                    break;
                default:
                    membro = membro.OrderBy(s => s.MembroId);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Membro>.CreateAsync(membro.AsNoTracking(), pageNumber ?? 1, pageSize));
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
