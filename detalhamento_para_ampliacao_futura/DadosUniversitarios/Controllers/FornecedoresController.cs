using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosUniversitarios.Models;
using DadosUniversitarios.Services;
using DadosUniversitarios.ViewModels;

namespace DadosUniversitarios.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedos
        public async Task<IActionResult> Index()
        {
            var contratos = await _context.Contratos
                .Include(c => c.Servico)
                .Include(c => c.Empresa)
                .Include(c => c.Periodicidade)
                .OrderBy(c => c.NumeroContrato)
                .ToListAsync();

            // Transformando os contratos em ContratosViewModel
            var contratosViewModel = contratos
            .Select(contrato => new ContratosViewModel
            {
                Empresa = contrato.Empresa,
                Contrato = contrato
            })
            .ToList();

            return View(contratosViewModel);
        }

        // GET: Fornecedos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Contratos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedos/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas.Include(g => g.Endereco), "Id", "Nome");
            ViewData["PeriodicidadeId"] = new SelectList(_context.Periodicidade, "Id", "Nome");
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome");


            return View();
        }

        // POST: Fornecedos/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contrato contrato)
        {

            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Fornecedos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Contratos.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroContrato,Periodicidade,ValorServico,DataPagamento,VencimentoContrato,Nome,CNPJ,Email,Telefone,NomeServico")] Contrato fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
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
            return View(fornecedor);
        }

        
        // POST: Fornecedos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Contratos.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Contratos.Remove(fornecedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Contratos.Any(e => e.Id == id);
        }
    }
}
