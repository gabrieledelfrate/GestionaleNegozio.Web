using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionaleNegozio.Data;
using GestionaleNegozio.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using GestionaleNegozio.Data.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionaleNegozio.Web.Controllers
{
    public class OrdiniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdiniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ordini
        public async Task<IActionResult> Index()
        {
            var ordini = _context.Ordini.Include(o => o.Cliente);
            return View(await ordini.ToListAsync());
        }

        // GET: Ordini/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordine == null)
            {
                return NotFound();
            }

            return View(ordine);
        }

        // GET: Ordini/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome");
            return View();
        }

        // POST: Ordini/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Data,Stato,ClienteID,IndirizzoSpedizione")] Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", ordine.ClienteID);
            return View(ordine);
        }

        // GET: Ordini/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini.FindAsync(id);
            if (ordine == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", ordine.ClienteID);
            return View(ordine);
        }

        // POST: Ordini/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Data,Stato,ClienteID,IndirizzoSpedizione")] Ordine ordine)
        {
            if (id != ordine.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdineExists(ordine.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", ordine.ClienteID);
            return View(ordine);
        }

        // GET: Ordini/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordine = await _context.Ordini
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordine == null)
            {
                return NotFound();
            }

            return View(ordine);
        }

        // POST: Ordini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordine = await _context.Ordini.FindAsync(id);
            _context.Ordini.Remove(ordine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdineExists(int id)
        {
            return _context.Ordini.Any(e => e.ID == id);
        }
    }
}
