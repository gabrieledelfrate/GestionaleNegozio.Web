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
    public class FattureController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FattureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fatture
        public async Task<IActionResult> Index()
        {
            var fatture = _context.Fatture.Include(f => f.Cliente).Include(f => f.Ordine);
            return View(await fatture.ToListAsync());
        }

        // GET: Fatture/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fattura = await _context.Fatture
                .Include(f => f.Cliente)
                .Include(f => f.Ordine)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fattura == null)
            {
                return NotFound();
            }

            return View(fattura);
        }

        // GET: Fatture/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome");
            ViewData["OrdineID"] = new SelectList(_context.Ordini, "ID", "ID");
            return View();
        }

        // POST: Fatture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Data,NumeroFattura,ClienteID,OrdineID,ImportoTotale")] Fattura fattura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fattura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", fattura.ClienteID);
            ViewData["OrdineID"] = new SelectList(_context.Ordini, "ID", "ID", fattura.OrdineID);
            return View(fattura);
        }

        // GET: Fatture/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fattura = await _context.Fatture.FindAsync(id);
            if (fattura == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", fattura.ClienteID);
            ViewData["OrdineID"] = new SelectList(_context.Ordini, "ID", "ID", fattura.OrdineID);
            return View(fattura);
        }

        // POST: Fatture/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Data,NumeroFattura,ClienteID,OrdineID,ImportoTotale")] Fattura fattura)
        {
            if (id != fattura.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fattura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FatturaExists(fattura.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clienti, "ID", "Nome", fattura.ClienteID);
            ViewData["OrdineID"] = new SelectList(_context.Ordini, "ID", "ID", fattura.OrdineID);
            return View(fattura);
        }

        // GET: Fatture/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fattura = await _context.Fatture
                .Include(f => f.Cliente)
                .Include(f => f.Ordine)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fattura == null)
            {
                return NotFound();
            }

            return View(fattura);
        }

        // POST: Fatture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fattura = await _context.Fatture.FindAsync(id);
            _context.Fatture.Remove(fattura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FatturaExists(int id)
        {
            return _context.Fatture.Any(e => e.ID == id);
        }
    }
}
