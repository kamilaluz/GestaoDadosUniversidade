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
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.OrderBy(d => d.Nome).ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .Include(c => c.Disciplinas)
                .Include(c => c.Pessoas)
                .ThenInclude(t => t.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Curso curso)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));            
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        // POST: Curso/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            _context.Update(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }


        //-----------------------MATRICULAR DISCIPLINAS----------------------------------//


        // GET: Curso/AddDisciplina
        public async Task<IActionResult> AddDisciplina(int? id)
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            ViewBag.CursoId = id;
            return View();
        }

        // POST: Curso/AddDisciplina        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDisciplina(int cursoId, List<int> disciplinaIds)
        {
            var curso = await _context.Cursos.Include(c => c.Disciplinas).FirstOrDefaultAsync(c => c.Id == cursoId);
            if (curso == null)
            {
                return NotFound();
            }

            // Iterar sobre os IDs das disciplinas selecionadas
            foreach (var disciplinaId in disciplinaIds)
            {
                var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);
                if (disciplina != null && !curso.Disciplinas.Contains(disciplina))
                {
                    curso.Disciplinas.Add(disciplina);
                }
            }

            _context.Update(curso);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = cursoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int cursoId, int disciplinaId)
        {
            // Carregar o curso com as disciplinas relacionadas
            var curso = await _context.Cursos
                                      .Include(c => c.Disciplinas)
                                      .FirstOrDefaultAsync(c => c.Id == cursoId);

            // Localizar a disciplina na lista de disciplinas do curso
            var disciplina = curso.Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
            if (disciplina != null)
            {
                // Remover a disciplina do curso
                curso.Disciplinas.Remove(disciplina);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = cursoId });
        }
    }
}
