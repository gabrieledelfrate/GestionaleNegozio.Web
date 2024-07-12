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
    public class SpedizioniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpedizioniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Spedizioni
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spedizioni.ToListAsync());
        }

        // GET: Spedizioni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizioni
                .FirstOrDefaultAsync(m => m.ID == id);
            if (spedizione == null)
            {
                return NotFound();
            }

            return View(spedizione);
        }

        // GET: Spedizioni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spedizioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataArrivo,Descrizione,Quantita,Stato,FornitoreID")] Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spedizione);
        }

        // GET: Spedizioni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizioni.FindAsync(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            return View(spedizione);
        }

        // POST: Spedizioni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataArrivo,Descrizione,Quantita,Stato,FornitoreID")] Spedizione spedizione)
        {
            if (id != spedizione.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spedizione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpedizioneExists(spedizione.ID))
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
            return View(spedizione);
        }

        // GET: Spedizioni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizioni
                .FirstOrDefaultAsync(m => m.ID == id);
            if (spedizione == null)
            {
                return NotFound();
            }

            return View(spedizione);
        }

        // POST: Spedizioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spedizione = await _context.Spedizioni.FindAsync(id);
            _context.Spedizioni.Remove(spedizione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpedizioneExists(int id)
        {
            return _context.Spedizioni.Any(e => e.ID == id);
        }
    }
}
