using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosUniversitarios.Models;
using DadosUniversitarios.Services;
using NuGet.Packaging.Signing;

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
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var alunos = from a in _context.Pessoas
                         .Where(p => p.Tipo.NomeTipo == "Aluno")
                         .OrderBy(a => a.NumeroMatricula)
                         select a;

            // Aplicar filtro se searchString não for nulo ou vazio
            if (!string.IsNullOrEmpty(searchString))
            {
                alunos = alunos.Where(a => a.Nome.ToLower().Contains(searchString.ToLower()));
            }

            return View(alunos.ToList());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Pessoas
                .Include(d => d.Disciplina)
                .Include(c => c.Curso)
                .Where(p => p.Tipo.NomeTipo == "Aluno")
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


        //-----------------------MATRICULA DISCIPLINA----------------------------------//


        // GET: Curso/AddDisciplina
        public async Task<IActionResult> AddDisciplina(int? id)
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Pessoas.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            ViewBag.AlunoId = id;
            return View();
        }

        // POST: Curso/AddDisciplina        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDisciplina(int alunoId, List<int> disciplinaIds)
        {
            var aluno = await _context.Pessoas.Include(c => c.Disciplina).FirstOrDefaultAsync(c => c.Id == alunoId);
            if (aluno == null)
            {
                return NotFound();
            }

           // Iterar sobre os IDs das disciplinas selecionadas
            foreach (var disciplinaId in disciplinaIds)
            {
                var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);
                if (disciplina != null && !aluno.Disciplina.Contains(disciplina))
                {
                    aluno.Disciplina.Add(disciplina);
                }
            }

            _context.Update(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = alunoId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int alunoId, int disciplinaId)
        {
            // Carregar o aluno com as disciplinas relacionadas
            var aluno = await _context.Pessoas
                                      .Include(c => c.Disciplina)
                                      .FirstOrDefaultAsync(c => c.Id == alunoId);

            // Localizar a disciplina na lista de disciplinas do curso
            var disciplina = aluno.Disciplina.FirstOrDefault(d => d.Id == disciplinaId);
            if (disciplina != null)
            {
                // Remover a disciplina do aluno
                aluno.Disciplina.Remove(disciplina);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = alunoId });
        }

        //-----------------------MATRICULA CURSO----------------------------------//


        // GET: Curso/AddCurso
        public async Task<IActionResult> AddCurso(int? id)
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Pessoas.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            ViewBag.AlunoId = id;
            return View();
        }

        // POST: Curso/AddCurso        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCurso(int alunoId, List<int> cursosIds)
        {
            var aluno = await _context.Pessoas.Include(c => c.Curso).FirstOrDefaultAsync(c => c.Id == alunoId);
            if (aluno == null)
            {
                return NotFound();
            }

            // Iterar sobre os IDs dos cursos selecionados
            foreach (var cursoId in cursosIds)
            {
                var curso = await _context.Cursos.FindAsync(cursoId);
                if (curso != null && !aluno.Curso.Contains(curso))
                {
                    aluno.Curso.Add(curso);
                }
            }

            _context.Update(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = alunoId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCurso(int alunoId, int cursoId)
        {
            // Carregar o aluno com os cursos relacionados
            var aluno = await _context.Pessoas
                                      .Include(c => c.Curso)
                                      .FirstOrDefaultAsync(c => c.Id == alunoId);

            // Localizar o curso na lista de cursos do aluno
            var curso = aluno.Curso.FirstOrDefault(d => d.Id == cursoId);
            if (curso != null)
            {
                // Remover curso do aluno
                aluno.Curso.Remove(curso);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = alunoId });
        }

    }
}
