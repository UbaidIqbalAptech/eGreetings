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
    public class CardDesignsController : Controller
    {
        private readonly EGreetingsContext _context;

        public CardDesignsController(EGreetingsContext context)
        {
            _context = context;
        }

        // GET: CardDesigns
        public async Task<IActionResult> Index()
        {
            var eGreetingsContext = _context.CardDesigns.Include(c => c.Template);
            return View(await eGreetingsContext.ToListAsync());
        }

        public IActionResult MainView()
        {
            return View();
        }


        // GET: CardDesigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CardDesigns == null)
            {
                return NotFound();
            }

            var cardDesign = await _context.CardDesigns
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.DesignId == id);
            if (cardDesign == null)
            {
                return NotFound();
            }

            return View(cardDesign);
        }

        // GET: CardDesigns/Create
        public IActionResult Create()
        {
            ViewData["TemplateId"] = new SelectList(_context.Templates, "TemplateId", "TemplateId");
            return View();
        }

        // POST: CardDesigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignId,TemplateId,DesignName,DesignImageUrl,Description")] CardDesign cardDesign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TemplateId"] = new SelectList(_context.Templates, "TemplateId", "TemplateId", cardDesign.TemplateId);
            return View(cardDesign);
        }

        // GET: CardDesigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CardDesigns == null)
            {
                return NotFound();
            }

            var cardDesign = await _context.CardDesigns.FindAsync(id);
            if (cardDesign == null)
            {
                return NotFound();
            }
            ViewData["TemplateId"] = new SelectList(_context.Templates, "TemplateId", "TemplateId", cardDesign.TemplateId);
            return View(cardDesign);
        }

        // POST: CardDesigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignId,TemplateId,DesignName,DesignImageUrl,Description")] CardDesign cardDesign)
        {
            if (id != cardDesign.DesignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardDesignExists(cardDesign.DesignId))
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
            ViewData["TemplateId"] = new SelectList(_context.Templates, "TemplateId", "TemplateId", cardDesign.TemplateId);
            return View(cardDesign);
        }

        // GET: CardDesigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CardDesigns == null)
            {
                return NotFound();
            }

            var cardDesign = await _context.CardDesigns
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.DesignId == id);
            if (cardDesign == null)
            {
                return NotFound();
            }

            return View(cardDesign);
        }

        // POST: CardDesigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CardDesigns == null)
            {
                return Problem("Entity set 'EGreetingsContext.CardDesigns'  is null.");
            }
            var cardDesign = await _context.CardDesigns.FindAsync(id);
            if (cardDesign != null)
            {
                _context.CardDesigns.Remove(cardDesign);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardDesignExists(int id)
        {
          return (_context.CardDesigns?.Any(e => e.DesignId == id)).GetValueOrDefault();
        }
    }
}
