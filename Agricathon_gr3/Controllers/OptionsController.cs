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
    public class OptionsController : Controller
    {
        private readonly VSContext _context;

        public OptionsController(VSContext context)
        {
            _context = context;
        }

        // GET: Options
        public async Task<IActionResult> Index()
        {
            var vSContext = _context.OptionDB.Include(o => o.Question);
            return View(await vSContext.ToListAsync());
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _context.OptionDB
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.OptionId == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OptionId,NameOption,QuestionId")] Option option)
        {
            if (ModelState.IsValid)
            {
                _context.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", option.QuestionId);
            return View(option);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _context.OptionDB.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", option.QuestionId);
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OptionId,NameOption,QuestionId")] Option option)
        {
            if (id != option.OptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(option);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.OptionId))
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
            ViewData["QuestionId"] = new SelectList(_context.QuestionDB, "QuestionId", "NameQuestion", option.QuestionId);
            return View(option);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _context.OptionDB
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.OptionId == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var option = await _context.OptionDB.FindAsync(id);
            _context.OptionDB.Remove(option);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionExists(int id)
        {
            return _context.OptionDB.Any(e => e.OptionId == id);
        }
    }
}
