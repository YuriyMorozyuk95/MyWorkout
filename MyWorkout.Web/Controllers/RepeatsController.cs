using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Web.Data;
using MyWorkout.Web.Data.Entity;

namespace MyWorkout.Web.Controllers
{
    public class RepeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Repeats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Repeats.Include(r => r.Exercise);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Repeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _context.Repeats
                .Include(r => r.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repeat == null)
            {
                return NotFound();
            }

            return View(repeat);
        }

        // GET: Repeats/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Id");
            return View();
        }

        // POST: Repeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Count,ExerciseId")] Repeat repeat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Id", repeat.ExerciseId);
            return View(repeat);
        }

        // GET: Repeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _context.Repeats.FindAsync(id);
            if (repeat == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Id", repeat.ExerciseId);
            return View(repeat);
        }

        // POST: Repeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Count,ExerciseId")] Repeat repeat)
        {
            if (id != repeat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepeatExists(repeat.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Id", repeat.ExerciseId);
            return View(repeat);
        }

        // GET: Repeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _context.Repeats
                .Include(r => r.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repeat == null)
            {
                return NotFound();
            }

            return View(repeat);
        }

        // POST: Repeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repeat = await _context.Repeats.FindAsync(id);
            _context.Repeats.Remove(repeat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepeatExists(int id)
        {
            return _context.Repeats.Any(e => e.Id == id);
        }
    }
}
