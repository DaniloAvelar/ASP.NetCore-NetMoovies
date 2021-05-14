using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetMoovies.Data;
using NetMoovies.Models;

namespace NetMoovies.Controllers
{
    public class FilmesController : Controller
    {
        private readonly NetMooviesContext _context;

        public FilmesController(NetMooviesContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index(string filmeGenero,string busca)
        {
            // Usando uma  consulta LINQ para retornar todos os gêneros dos filmes
            IQueryable<string> consultaGenero = from g in _context.Filme
                                                orderby g.Genero
                                                select g.Genero;

            // Consulta LINQ para selecionar filmes : Aqui a consulta foi apenas definida e não executada ainda.
            var filmes = from m in _context.Filme
                         select m;

            //verificamos se o parâmetro criterioBusca não for vazio nem nulo a consulta de
            //filmes será modificada para filtrar pelo critério informado o titulo dos filmes
            if (!String.IsNullOrEmpty(busca))
            {
                filmes = filmes.Where(f => f.Titulo.Contains(busca));
            }

            //verificamos se o parâmetro Genero não for vazio nem nulo a consulta
            if (!String.IsNullOrEmpty(filmeGenero))
            {
                filmes = filmes.Where(z => z.Genero == filmeGenero);
            }

            var filmeGeneroVM = new FilmeGeneroViewModel();
            filmeGeneroVM.generos = new SelectList(await consultaGenero.Distinct().ToListAsync());
            filmeGeneroVM.filmes = await filmes.ToListAsync();

            return View(filmeGeneroVM);

          //return View(await filmes.ToListAsync());
          //return View(await _context.Filme.ToListAsync()); *** Não trazer contexto completo, e sim a variavel de busca = filmes
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Lancamento,Genero,Preco,Classificacao")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Lancamento,Genero,Preco,Classificacao")] Filme filme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.Id))
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
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
    }
}
