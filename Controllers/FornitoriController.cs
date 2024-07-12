using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionaleNegozio.Data;
using GestionaleNegozio.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using GestionaleNegozio.Data.Contexts;
using GestionaleNegozio.Data.Models.GestionaleNegozio.Data.Models;

namespace GestionaleNegozio.Web.Controllers
{
    public class FornitoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornitoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fornitori
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornitori.ToListAsync());
        }

        // GET: Fornitori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornitore = await _context.Fornitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fornitore == null)
            {
                return NotFound();
            }

            return View(fornitore);
        }

        // GET: Fornitori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornitori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Indirizzo,Email,Telefono")] Fornitore fornitore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornitore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornitore);
        }

        // GET: Fornitori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornitore = await _context.Fornitori.FindAsync(id);
            if (fornitore == null)
            {
                return NotFound();
            }
            return View(fornitore);
        }

        // POST: Fornitori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Indirizzo,Email,Telefono")] Fornitore fornitore)
        {
            if (id != fornitore.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornitore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornitoreExists(fornitore.ID))
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
            return View(fornitore);
        }

        // GET: Fornitori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornitore = await _context.Fornitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fornitore == null)
            {
                return NotFound();
            }

            return View(fornitore);
        }

        // POST: Fornitori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornitore = await _context.Fornitori.FindAsync(id);
            _context.Fornitori.Remove(fornitore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornitoreExists(int id)
        {
            return _context.Fornitori.Any(e => e.ID == id);
        }
    }
}
