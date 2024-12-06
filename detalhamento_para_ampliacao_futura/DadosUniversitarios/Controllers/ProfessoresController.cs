using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosUniversitarios.Models;
using DadosUniversitarios.Services;
using Microsoft.AspNetCore.Authorization;

namespace DadosUniversitarios.Controllers
{
    [Authorize]
    public class ProfessoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Professores
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var professores = from a in _context.Pessoas
                         .Where(p => p.Tipo.NomeTipo == "Professor")
                         .OrderBy(a => a.NumeroMatricula)
                         select a;

            // Aplicar filtro se searchString não for nulo ou vazio
            if (!string.IsNullOrEmpty(searchString))
            {
                professores = professores.Where(a => a.Nome.ToLower().Contains(searchString.ToLower()));
            }

            return View(professores.ToList());            
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Pessoas
                .Include(d => d.Disciplina)
                .Include(c => c.Curso)
                .Where(p => p.Tipo.NomeTipo == "Professor")
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

        //-----------------------MATRICULA DISCIPLINA----------------------------------//


        // GET: Curso/AddDisciplina
        public async Task<IActionResult> AddDisciplina(int? id)
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Pessoas.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            ViewBag.ProfessorId = id;
            return View();
        }

        // POST: Curso/AddDisciplina        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDisciplina(int professorId, List<int> disciplinaIds)
        {
            var professor = await _context.Pessoas.Include(c => c.Disciplina).FirstOrDefaultAsync(c => c.Id == professorId);
            if (professor == null)
            {
                return NotFound();
            }

            // Iterar sobre os IDs das disciplinas selecionadas
            foreach (var disciplinaId in disciplinaIds)
            {
                var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);
                if (disciplina != null && !professor.Disciplina.Contains(disciplina))
                {
                    professor.Disciplina.Add(disciplina);
                }
            }

            _context.Update(professor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = professorId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int professorId, int disciplinaId)
        {
            // Carregar o aluno com as disciplinas relacionadas
            var professor = await _context.Pessoas
                                      .Include(c => c.Disciplina)
                                      .FirstOrDefaultAsync(c => c.Id == professorId);

            // Localizar a disciplina na lista de disciplinas do curso
            var disciplina = professor.Disciplina.FirstOrDefault(d => d.Id == disciplinaId);
            if (disciplina != null)
            {
                // Remover a disciplina do aluno
                professor.Disciplina.Remove(disciplina);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = professorId });
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

            var professor = await _context.Pessoas.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            ViewBag.ProfessorId = id;
            return View();
        }

        // POST: Curso/AddCurso        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCurso(int professorId, List<int> cursosIds)
        {
            var professor = await _context.Pessoas.Include(c => c.Curso).FirstOrDefaultAsync(c => c.Id == professorId);
            if (professor == null)
            {
                return NotFound();
            }

            // Iterar sobre os IDs dos cursos selecionados
            foreach (var cursoId in cursosIds)
            {
                var curso = await _context.Cursos.FindAsync(cursoId);
                if (curso != null && !professor.Curso.Contains(curso))
                {
                    professor.Curso.Add(curso);
                }
            }

            _context.Update(professor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = professorId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCurso(int professorId, int cursoId)
        {
            // Carregar o aluno com os cursos relacionados
            var professor = await _context.Pessoas
                                      .Include(c => c.Curso)
                                      .FirstOrDefaultAsync(c => c.Id == professorId);

            // Localizar o curso na lista de cursos do aluno
            var curso = professor.Curso.FirstOrDefault(d => d.Id == cursoId);
            if (curso != null)
            {
                // Remover curso do aluno
                professor.Curso.Remove(curso);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = professorId });
        }
    }
}
