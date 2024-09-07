using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGreetings.Models;

namespace eGreetings.Controllers
{
    public class DesignElementsController : Controller
    {
        private readonly EGreetingsContext _context;

        public DesignElementsController(EGreetingsContext context)
        {
            _context = context;
        }

        // GET: DesignElements
        public async Task<IActionResult> Index()
        {
            var eGreetingsContext = _context.DesignElements.Include(d => d.Design);
            return View(await eGreetingsContext.ToListAsync());
        }

        // GET: DesignElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DesignElements == null)
            {
                return NotFound();
            }

            var designElement = await _context.DesignElements
                .Include(d => d.Design)
                .FirstOrDefaultAsync(m => m.ElementId == id);
            if (designElement == null)
            {
                return NotFound();
            }

            return View(designElement);
        }

        // GET: DesignElements/Create
        public IActionResult Create()
        {
            ViewData["DesignId"] = new SelectList(_context.CardDesigns, "DesignId", "DesignId");
            return View();
        }

        // POST: DesignElements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElementId,DesignId,ElementType,ContentUrl,TextContent,PositionX,PositionY,Width,Height")] DesignElement designElement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designElement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignId"] = new SelectList(_context.CardDesigns, "DesignId", "DesignId", designElement.DesignId);
            return View(designElement);
        }

        // GET: DesignElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DesignElements == null)
            {
                return NotFound();
            }

            var designElement = await _context.DesignElements.FindAsync(id);
            if (designElement == null)
            {
                return NotFound();
            }
            ViewData["DesignId"] = new SelectList(_context.CardDesigns, "DesignId", "DesignId", designElement.DesignId);
            return View(designElement);
        }

        // POST: DesignElements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElementId,DesignId,ElementType,ContentUrl,TextContent,PositionX,PositionY,Width,Height")] DesignElement designElement)
        {
            if (id != designElement.ElementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignElementExists(designElement.ElementId))
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
            ViewData["DesignId"] = new SelectList(_context.CardDesigns, "DesignId", "DesignId", designElement.DesignId);
            return View(designElement);
        }

        // GET: DesignElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DesignElements == null)
            {
                return NotFound();
            }

            var designElement = await _context.DesignElements
                .Include(d => d.Design)
                .FirstOrDefaultAsync(m => m.ElementId == id);
            if (designElement == null)
            {
                return NotFound();
            }

            return View(designElement);
        }

        // POST: DesignElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DesignElements == null)
            {
                return Problem("Entity set 'EGreetingsContext.DesignElements'  is null.");
            }
            var designElement = await _context.DesignElements.FindAsync(id);
            if (designElement != null)
            {
                _context.DesignElements.Remove(designElement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignElementExists(int id)
        {
          return (_context.DesignElements?.Any(e => e.ElementId == id)).GetValueOrDefault();
        }
    }
}
