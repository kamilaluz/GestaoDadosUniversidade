using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosUniversitarios.Models;
using DadosUniversitarios.Services;

namespace DadosUniversitarios.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empresa
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var empresas = from a in _context.Empresas
                         .Include(s => s.NomeServico).OrderBy(a => a.Nome)
                         select a;

            // Aplicar filtro se searchString não for nulo ou vazio
            if (!string.IsNullOrEmpty(searchString))
            {
                empresas = empresas.Where(a => a.Nome.ToLower().Contains(searchString.ToLower()));
            }

            return View(empresas.ToList());
        }

        // GET: Empresa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(c => c.Contratos)
                    .ThenInclude(e => e.Servico)
                .Include(c => c.Contratos)
                    .ThenInclude(e => e.Periodicidade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresa/Create
        public IActionResult Create()
        {
            ViewData["NomeServicoId"] = new SelectList(_context.Servicos, "Id", "Nome");
            var empresa = new Empresa
            {
                Endereco = new Endereco() // Inicializa a propriedade Endereco
            };
            return View(empresa);
        }

        // POST: Empresa/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa, Endereco endereco)
        {
            // Busca pelo endereço com base na rua
            var enderecoExistente = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.NomeRua == endereco.NomeRua);

            if (enderecoExistente == null)
            {
                // Caso o endereço não exista, cria um novo
                Endereco novoEndereco = new Endereco
                {
                    Cep = endereco.Cep,
                    NomeRua = endereco.NomeRua,
                    Bairro = endereco.Bairro,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado
                };

                // Adiciona o novo endereço ao banco de dados
                _context.Enderecos.Add(novoEndereco);
                await _context.SaveChangesAsync(); // Salva para gerar o ID do endereço

                empresa.EnderecoId = novoEndereco.Id; // Atribui o ID do novo endereço ao aluno

            }
            else
            {
                // Se o endereço já existir, utiliza o ID encontrado
                empresa.EnderecoId = enderecoExistente.Id;
            }

            // Adiciona o aluno com o EnderecoId correto
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    

        // GET: Empresa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["NomeServicoId"] = new SelectList(_context.Servicos, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresa/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            _context.Update(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
