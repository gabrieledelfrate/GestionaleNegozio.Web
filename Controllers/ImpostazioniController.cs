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
    public class ImpostazioniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImpostazioniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Impostazioni
        public async Task<IActionResult> Index()
        {
            return View(await _context.Impostazioni.ToListAsync());
        }

        // GET: Impostazioni/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impostazioni = await _context.Impostazioni.FindAsync(id);
            if (impostazioni == null)
            {
                return NotFound();
            }
            return View(impostazioni);
        }

        // POST: Impostazioni/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeNegozio,Logo,InformazioniContatto,ImpostazioniSpedizione,ImpostazioniPagamento")] Impostazioni impostazioni)
        {
            if (id != impostazioni.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(impostazioni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpostazioniExists(impostazioni.ID))
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
            return View(impostazioni);
        }

        private bool ImpostazioniExists(int id)
        {
            return _context.Impostazioni.Any(e => e.ID == id);
        }
    }
}
