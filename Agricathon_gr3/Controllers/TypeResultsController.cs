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
    public class TypeResultsController : Controller
    {
        private readonly VSContext _context;

        public TypeResultsController(VSContext context)
        {
            _context = context;
        }

        // GET: TypeResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeResultDB.ToListAsync());
        }

        // GET: TypeResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeResult = await _context.TypeResultDB
                .FirstOrDefaultAsync(m => m.TypeRId == id);
            if (typeResult == null)
            {
                return NotFound();
            }

            return View(typeResult);
        }

        // GET: TypeResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeRId,TypeR")] TypeResult typeResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeResult);
        }

        // GET: TypeResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeResult = await _context.TypeResultDB.FindAsync(id);
            if (typeResult == null)
            {
                return NotFound();
            }
            return View(typeResult);
        }

        // POST: TypeResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeRId,TypeR")] TypeResult typeResult)
        {
            if (id != typeResult.TypeRId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeResultExists(typeResult.TypeRId))
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
            return View(typeResult);
        }

        // GET: TypeResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeResult = await _context.TypeResultDB
                .FirstOrDefaultAsync(m => m.TypeRId == id);
            if (typeResult == null)
            {
                return NotFound();
            }

            return View(typeResult);
        }

        // POST: TypeResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeResult = await _context.TypeResultDB.FindAsync(id);
            _context.TypeResultDB.Remove(typeResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeResultExists(int id)
        {
            return _context.TypeResultDB.Any(e => e.TypeRId == id);
        }
    }
}
