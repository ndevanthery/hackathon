using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agricathon_gr3.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Agricathon_gr3.Controllers
{
    public class QuestionnairesController : Controller
    {
        private readonly VSContext _context;
        private SignInManager<IdentityUser> SignInManager;

        public QuestionnairesController(VSContext context)
        {
            _context = context;
        }

        // GET: Questionnaires

        public async Task<IActionResult> Index(int projectId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId is not null)
            {
                HttpContext.Session.SetInt32("projectId", projectId);
        
                return View(projectId);
            }
            else
                return RedirectToAction("Index", "Home");
                
        }
        public async Task<IActionResult> List()
        {
            var vSContext = _context.QuestionnaireDB.Include(q => q.Phase).Include(q => q.Project);
            return View(await vSContext.ToListAsync());
        }

        // GET: Questionnaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.QuestionnaireDB
                .Include(q => q.Phase)
                .Include(q => q.Project)
                .FirstOrDefaultAsync(m => m.QuestionnaireId == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // GET: Questionnaires/Create
        public IActionResult Create()
        {
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase");
            ViewData["ProjectId"] = new SelectList(_context.ProjectDB, "ProjectId", "NameProject");
            return View();
        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionnaireId,ProjectId,NameQuestionnaire,Date,PersonId,PhaseId,TypeRId")] Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", questionnaire.PhaseId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectDB, "ProjectId", "NameProject", questionnaire.ProjectId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.QuestionnaireDB.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", questionnaire.PhaseId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectDB, "ProjectId", "NameProject", questionnaire.ProjectId);
            return View(questionnaire);
        }

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionnaireId,ProjectId,NameQuestionnaire,Date,PersonId,PhaseId,TypeRId")] Questionnaire questionnaire)
        {
            if (id != questionnaire.QuestionnaireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireExists(questionnaire.QuestionnaireId))
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
            ViewData["PhaseId"] = new SelectList(_context.PhaseDB, "PhaseId", "NamePhase", questionnaire.PhaseId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectDB, "ProjectId", "NameProject", questionnaire.ProjectId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.QuestionnaireDB
                .Include(q => q.Phase)
                .Include(q => q.Project)
                .FirstOrDefaultAsync(m => m.QuestionnaireId == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaire = await _context.QuestionnaireDB.FindAsync(id);
            _context.QuestionnaireDB.Remove(questionnaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireExists(int id)
        {
            return _context.QuestionnaireDB.Any(e => e.QuestionnaireId == id);
        }

        private bool QuestionnaireExistsPhaseAndType(int idPhase, int type)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _context.QuestionnaireDB.Where(e=> e.PersonId == currentUserId).Any(e => e.PhaseId == idPhase || e.TypeRId == type);
        }

        public async Task<IActionResult> socialButton()
        {
            var project = HttpContext.Session.GetInt32("projectId");
            int idPhase = _context.ProjectDB.Where(e => e.ProjectId == project).Select(e => e.PhaseId).First();
            if (QuestionnaireExistsPhaseAndType(idPhase, 2))
            {
                return RedirectToAction("Questionnaires", "Edit");
            }
            else
            {
                return RedirectToAction("Questionnaires", "Create");
            }

              
            
        }
    }
}
