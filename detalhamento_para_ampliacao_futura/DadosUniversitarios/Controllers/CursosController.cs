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
        public async Task<IActionResult> AddDisciplina(int cursoId, int disciplinaId)
        {
            var curso = await _context.Cursos.Include(c => c.Disciplinas).FirstOrDefaultAsync(c => c.Id == cursoId);
            if (curso == null)
            {
                return NotFound();
            }

            // Buscar a disciplina selecionada
            var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);
            if (disciplina == null)
            {
                return NotFound();
            }


            // Verificar se a disciplina já está vinculada ao curso
            if (!curso.Disciplinas.Contains(disciplina))
            {
                curso.Disciplinas.Add(disciplina); // Adicionar a disciplina ao curso
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = cursoId });
        }
    }
}
