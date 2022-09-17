using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GateRiddleSolver.Data;
using GateRiddleSolver.Models;

namespace GateRiddleSolver.Controllers
{
    public class TranslationRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranslationRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TranslationRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TranslationRecords.Include(t => t.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TranslationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TranslationRecords == null)
            {
                return NotFound();
            }

            var translationRecord = await _context.TranslationRecords
                .Include(t => t.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translationRecord == null)
            {
                return NotFound();
            }

            return View(translationRecord);
        }

        // GET: TranslationRecords/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id");
            return View();
        }

        // POST: TranslationRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TextToTranslate,TranslationResult,LanguageId")] TranslationRecord translationRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(translationRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", translationRecord.LanguageId);
            return View(translationRecord);
        }

        // GET: TranslationRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TranslationRecords == null)
            {
                return NotFound();
            }

            var translationRecord = await _context.TranslationRecords.FindAsync(id);
            if (translationRecord == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", translationRecord.LanguageId);
            return View(translationRecord);
        }

        // POST: TranslationRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TextToTranslate,TranslationResult,LanguageId")] TranslationRecord translationRecord)
        {
            if (id != translationRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(translationRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationRecordExists(translationRecord.Id))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", translationRecord.LanguageId);
            return View(translationRecord);
        }

        // GET: TranslationRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TranslationRecords == null)
            {
                return NotFound();
            }

            var translationRecord = await _context.TranslationRecords
                .Include(t => t.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translationRecord == null)
            {
                return NotFound();
            }

            return View(translationRecord);
        }

        // POST: TranslationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TranslationRecords == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TranslationRecords'  is null.");
            }
            var translationRecord = await _context.TranslationRecords.FindAsync(id);
            if (translationRecord != null)
            {
                _context.TranslationRecords.Remove(translationRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranslationRecordExists(int id)
        {
          return _context.TranslationRecords.Any(e => e.Id == id);
        }
    }
}
