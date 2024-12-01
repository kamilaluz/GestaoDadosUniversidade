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
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {

            return View(await _context.Pessoas
                .Where(p => p.Tipo.NomeTipo == "Aluno")
                .OrderBy(a => a.NumeroMatricula).ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Pessoas.Where(p => p.Tipo.NomeTipo == "Aluno")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            var aluno = new Pessoa
            {
                Endereco = new Endereco(),
                TipoId = 1
            };

            return View(aluno);
        }

        // POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pessoa aluno, Endereco endereco)
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

                aluno.EnderecoId = novoEndereco.Id; // Atribui o ID do novo endereço ao aluno
                
            }
            else
            {
                // Se o endereço já existir, utiliza o ID encontrado
                aluno.EnderecoId = enderecoExistente.Id;
            }

            // Adiciona o aluno com o EnderecoId correto
            _context.Pessoas.Add(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var aluno = await _context.Pessoas
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            _context.Update(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
                
        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Pessoas.FindAsync(id);
            if (aluno != null)
            {
                _context.Pessoas.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
