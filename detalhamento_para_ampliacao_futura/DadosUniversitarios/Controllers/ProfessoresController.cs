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
    public class ProfessoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Professores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pessoas
                .Where(p => p.Tipo.NomeTipo == "Professor")
                .OrderBy(a => a.NumeroMatricula).ToListAsync());
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            var professor = new Pessoa
            {
                Endereco = new Endereco(),
                TipoId = 2
            };
            return View(professor);
        }

        // POST: Professores/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa professor, Endereco endereco)
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

                professor.EnderecoId = novoEndereco.Id; // Atribui o ID do novo endereço ao aluno

            }
            else
            {
                // Se o endereço já existir, utiliza o ID encontrado
                professor.EnderecoId = enderecoExistente.Id;
            }

            // Adiciona o aluno com o EnderecoId correto
            _context.Pessoas.Add(professor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Pessoas
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }
            _context.Update(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Professores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Pessoas.FindAsync(id);
            if (professor != null)
            {
                _context.Pessoas.Remove(professor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
