using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleverIgreja.Models;
using CleverIgreja.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CleverIgreja.Controllers
{
    public class FinanceiroController : Controller
    {
        private readonly BdSystemContext _context;

        public FinanceiroController(BdSystemContext context)
        {
            _context = context;
        }

        // GET: Financeiro
        public async Task<IActionResult> Index()
        {
            var bdSystemContext = _context.Financeiro.Include(f => f.Categoria).Include(f => f.Moeda);
            return View(await bdSystemContext.ToListAsync());
        }

        // GET: Financeiro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Financeiro == null)
            {
                return NotFound();
            }

            var financeiro = await _context.Financeiro
                .Include(f => f.Categoria)
                .Include(f => f.Moeda)
                .FirstOrDefaultAsync(m => m.FinanceiroId == id);
            if (financeiro == null)
            {
                return NotFound();
            }

            return View(financeiro);
        }

        // GET: Financeiro/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId");
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId");
            return View();
        }

        // POST: Financeiro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FinanceiroId,DtLanc,DtVenc,DtPagto,DtCompetencia,TipoMovimentacaoFinanceira,NumeroDocumento,ParceiroId,Parceiro,Historico,Valor,Desconto,Juros,Total,MoedaId,CategoriaId")] Financeiro financeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId", financeiro.CategoriaId);
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId", financeiro.MoedaId);
            return View(financeiro);
        }

        // GET: Financeiro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Financeiro == null)
            {
                return NotFound();
            }

            var financeiro = await _context.Financeiro.FindAsync(id);
            if (financeiro == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId", financeiro.CategoriaId);
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId", financeiro.MoedaId);
            return View(financeiro);
        }

        // POST: Financeiro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FinanceiroId,DtLanc,DtVenc,DtPagto,DtCompetencia,TipoMovimentacaoFinanceira,NumeroDocumento,ParceiroId,Parceiro,Historico,Valor,Desconto,Juros,Total,MoedaId,CategoriaId")] Financeiro financeiro)
        {
            if (id != financeiro.FinanceiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinanceiroExists(financeiro.FinanceiroId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId", financeiro.CategoriaId);
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId", financeiro.MoedaId);
            return View(financeiro);
        }

        // GET: Financeiro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Financeiro == null)
            {
                return NotFound();
            }

            var financeiro = await _context.Financeiro
                .Include(f => f.Categoria)
                .Include(f => f.Moeda)
                .FirstOrDefaultAsync(m => m.FinanceiroId == id);
            if (financeiro == null)
            {
                return NotFound();
            }

            return View(financeiro);
        }

        // POST: Financeiro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Financeiro == null)
            {
                return Problem("Entity set 'BdSystemContext.Financeiro'  is null.");
            }
            var financeiro = await _context.Financeiro.FindAsync(id);
            if (financeiro != null)
            {
                _context.Financeiro.Remove(financeiro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinanceiroExists(int id)
        {
            return (_context.Financeiro?.Any(e => e.FinanceiroId == id)).GetValueOrDefault();
        }

        // GET: Membro
        public async Task<IActionResult> LancarContribuicaoIndex(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
        // GET: Financeiro/Create
        public IActionResult CadastrarContribuicao()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId");
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId");
            return View();
        }

        // POST: Financeiro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarContribuicao([Bind("FinanceiroId,DtLanc,DtVenc,DtPagto,DtCompetencia,TipoMovimentacaoFinanceira,NumeroDocumento,ParceiroId,Parceiro,Historico,Valor,Desconto,Juros,Total,MoedaId,CategoriaId")] Financeiro financeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "CategoriaId", "CategoriaId", financeiro.CategoriaId);
            ViewData["MoedaId"] = new SelectList(_context.Set<Moeda>(), "MoedaId", "MoedaId", financeiro.MoedaId);
            return View(financeiro);
        }
    }
}
