using System;
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
    public class ContribuicaoController : Controller
    {
        private readonly BdSystemContext _context;
        public ContribuicaoController(BdSystemContext context)
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
    }
}
