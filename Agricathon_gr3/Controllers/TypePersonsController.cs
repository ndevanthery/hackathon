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
    public class TypePersonsController : Controller
    {
        private readonly VSContext _context;

        public TypePersonsController(VSContext context)
        {
            _context = context;
        }

        // GET: TypePersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypePersonDB.ToListAsync());
        }

        // GET: TypePersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePerson = await _context.TypePersonDB
                .FirstOrDefaultAsync(m => m.TypePId == id);
            if (typePerson == null)
            {
                return NotFound();
            }

            return View(typePerson);
        }

        // GET: TypePersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypePId,TypeP")] TypePerson typePerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typePerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typePerson);
        }

        // GET: TypePersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePerson = await _context.TypePersonDB.FindAsync(id);
            if (typePerson == null)
            {
                return NotFound();
            }
            return View(typePerson);
        }

        // POST: TypePersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypePId,TypeP")] TypePerson typePerson)
        {
            if (id != typePerson.TypePId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typePerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypePersonExists(typePerson.TypePId))
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
            return View(typePerson);
        }

        // GET: TypePersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePerson = await _context.TypePersonDB
                .FirstOrDefaultAsync(m => m.TypePId == id);
            if (typePerson == null)
            {
                return NotFound();
            }

            return View(typePerson);
        }

        // POST: TypePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typePerson = await _context.TypePersonDB.FindAsync(id);
            _context.TypePersonDB.Remove(typePerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypePersonExists(int id)
        {
            return _context.TypePersonDB.Any(e => e.TypePId == id);
        }
    }
}
