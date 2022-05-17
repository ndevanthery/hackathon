using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agricathon_gr3.Models;

namespace Agricathon_gr3.Controllers
{
    public class AnwsersController : Controller
    {
        private readonly VSContext _context;

        public AnwsersController(VSContext context)
        {
            _context = context;
        }

        // GET: Anwsers
        public async Task<IActionResult> Index()
        {
            var vSContext = _context.AnwserDB.Include(a => a.Question).Include(a => a.Questionnaire);
            return View(await vSContext.ToListAsync());
        }

        // GET: Anwsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anwser = await _context.AnwserDB
                .Include(a => a.Question)
                .Include(a => a.Questionnaire)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (anwser == null)
            {
                return NotFound();
            }

            return View(anwser);
        }

        // GET: Anwsers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion");
            ViewData["QuestionnaireId"] = new SelectList(_context.QuestionnaireDB, "QuestionnaireId", "NameQuestionnaire");
            return View();
        }

        // POST: Anwsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionnaireId,QuestionId,Response")] Anwser anwser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anwser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", anwser.QuestionId);
            ViewData["QuestionnaireId"] = new SelectList(_context.QuestionnaireDB, "QuestionnaireId", "NameQuestionnaire", anwser.QuestionnaireId);
            return View(anwser);
        }

        // GET: Anwsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anwser = await _context.AnwserDB.FindAsync(id);
            if (anwser == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", anwser.QuestionId);
            ViewData["QuestionnaireId"] = new SelectList(_context.QuestionnaireDB, "QuestionnaireId", "NameQuestionnaire", anwser.QuestionnaireId);
            return View(anwser);
        }

        // POST: Anwsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionnaireId,QuestionId,Response")] Anwser anwser)
        {
            if (id != anwser.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anwser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnwserExists(anwser.QuestionId))
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
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", anwser.QuestionId);
            ViewData["QuestionnaireId"] = new SelectList(_context.QuestionnaireDB, "QuestionnaireId", "NameQuestionnaire", anwser.QuestionnaireId);
            return View(anwser);
        }

        // GET: Anwsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anwser = await _context.AnwserDB
                .Include(a => a.Question)
                .Include(a => a.Questionnaire)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (anwser == null)
            {
                return NotFound();
            }

            return View(anwser);
        }

        // POST: Anwsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anwser = await _context.AnwserDB.FindAsync(id);
            _context.AnwserDB.Remove(anwser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnwserExists(int id)
        {
            return _context.AnwserDB.Any(e => e.QuestionId == id);
        }
    }
}
