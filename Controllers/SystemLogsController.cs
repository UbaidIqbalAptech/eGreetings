﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGreetings.Models;

namespace eGreetings.Controllers
{
    public class SystemLogsController : Controller
    {
        private readonly EGreetingsContext _context;

        public SystemLogsController(EGreetingsContext context)
        {
            _context = context;
        }

        // GET: SystemLogs
        public async Task<IActionResult> Index()
        {
            var eGreetingsContext = _context.SystemLogs.Include(s => s.User);
            return View(await eGreetingsContext.ToListAsync());
        }

        // GET: SystemLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemLogs == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLogs
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (systemLog == null)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // GET: SystemLogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: SystemLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,LogDate,LogLevel,Message,UserId")] SystemLog systemLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", systemLog.UserId);
            return View(systemLog);
        }

        // GET: SystemLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemLogs == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLogs.FindAsync(id);
            if (systemLog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", systemLog.UserId);
            return View(systemLog);
        }

        // POST: SystemLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,LogDate,LogLevel,Message,UserId")] SystemLog systemLog)
        {
            if (id != systemLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemLogExists(systemLog.LogId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", systemLog.UserId);
            return View(systemLog);
        }

        // GET: SystemLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemLogs == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLogs
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (systemLog == null)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // POST: SystemLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemLogs == null)
            {
                return Problem("Entity set 'EGreetingsContext.SystemLogs'  is null.");
            }
            var systemLog = await _context.SystemLogs.FindAsync(id);
            if (systemLog != null)
            {
                _context.SystemLogs.Remove(systemLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemLogExists(int id)
        {
          return (_context.SystemLogs?.Any(e => e.LogId == id)).GetValueOrDefault();
        }
    }
}
