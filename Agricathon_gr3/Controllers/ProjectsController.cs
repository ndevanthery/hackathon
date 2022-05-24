using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agricathon_gr3.Models;
using System.Security.Claims;

namespace Agricathon_gr3.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly VSContext _context;

        public ProjectsController(VSContext context)
        {
            _context = context;
        }

        //GET : Projects by user ID
        public async Task<IActionResult> MyProjects()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var List = _context.PersProjectDB;
            List<Project> ListProject = new List<Project>();
            foreach(var m in List)
            {
                if(m.PersonId == currentUserId)
                {
                    ListProject.Add(m.Project);
                }
            }
            //Where(m=>m.PersonId==currentUserId).ToListAsync();
            return View(ListProject);
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<PersProject> List =await _context.PersProjectDB.ToListAsync();
            List<Project> ListIdProject = new List<Project>();
            foreach (PersProject m in List)
            {
                if (m.PersonId == currentUserId)
                {
                    ListIdProject.Add(await _context.ProjectDB.FirstOrDefaultAsync(p=> p.ProjectId == m.ProjectId ));
                }
            }
            //Where(m=>m.PersonId==currentUserId).ToListAsync();
            return View(ListIdProject);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.ProjectDB
                .Include(p => p.Phase)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,NameProject,Place,PhaseId")] Project project)
        {
            if (ModelState.IsValid)
            {
                var projetAdded=  _context.Add(project);
                await _context.SaveChangesAsync();

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                PersProject test = new PersProject
                {
                    PersonId = currentUserId,
                    ProjectId = projetAdded.Entity.ProjectId,
                };
                _context.Add(test);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", project.PhaseId);


            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.ProjectDB.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", project.PhaseId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,NameProject,Place,PhaseId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", project.PhaseId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.ProjectDB
                .Include(p => p.Phase)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.ProjectDB.FindAsync(id);
            _context.ProjectDB.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.ProjectDB.Any(e => e.ProjectId == id);
        }
    }
}
