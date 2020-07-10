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
    using Data.Repositories;

    public class RepeatsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepeatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Repeats
        public async Task<IActionResult> Index()
        {
            var repeats = await _unitOfWork.RepeatRepository.ReadAll().ToListAsync();
            repeats.ForEach(r => _unitOfWork.Load(r, e => e.Exercise));

            return View(repeats);
        }

        // GET: Repeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _unitOfWork.RepeatRepository.Read(id.Value);
            _unitOfWork.Load(repeat, e => e.Exercise);

            if (repeat == null)
            {
                return NotFound();
            }

            return View(repeat);
        }

        // GET: Repeats/Create
        public async Task<IActionResult> Create()
        {
            var exercises = await _unitOfWork.ExerciseRepository.ReadAll().ToListAsync();
            ViewBag.ExerciseItems = new SelectList(exercises, "Id", "Id");

            var repeat = new Repeat();

            return View(repeat);
        }

        // GET: Repeats/CreateFromExercise/328
        public IActionResult CreateFromExercise(int? exerciseId, int number)
        {
            if(exerciseId == default(int))
            {  
                throw new ArgumentNullException("excersise id is not set");
            }

            var repeat = new Repeat()
            {
                Number = number,
                ExerciseId = exerciseId.Value
            };

            return View(nameof(Create), repeat);
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
                await _unitOfWork.RepeatRepository.Create(repeat);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(ExercisesController.Details), "Exercises", repeat.ExerciseId);
            }
            var exercises = await _unitOfWork.ExerciseRepository.ReadAll().ToListAsync();
            ViewData["ExerciseId"] = new SelectList(exercises, "Id", "Id", repeat.ExerciseId);

            return View(repeat);
        }

        // GET: Repeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _unitOfWork.RepeatRepository.Read(id.Value);
            if (repeat == null)
            {
                return NotFound();
            }

            var exercises = await _unitOfWork.ExerciseRepository.ReadAll().ToListAsync();
            ViewData["ExerciseId"] = new SelectList(exercises, "Id", "Id", repeat.ExerciseId);

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
                    _unitOfWork.RepeatRepository.Update(repeat);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RepeatExists(repeat.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(ExercisesController.Details), "Exercises", repeat.ExerciseId);
            }
            var exercises = await _unitOfWork.ExerciseRepository.ReadAll().ToListAsync();
            ViewData["ExerciseId"] = new SelectList(exercises, "Id", "Id", repeat.ExerciseId);

            return View(repeat);
        }

        // GET: Repeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeat = await _unitOfWork.RepeatRepository.Read(id.Value);
            _unitOfWork.Load(repeat, r => r.Exercise);

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
            await _unitOfWork.RepeatRepository.Delete(id);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(ExercisesController.Details), "Exercises",id);
        }

        private Task<bool> RepeatExists(int id)
        {
            return _unitOfWork.RepeatRepository.IsExist(e => e.Id == id);
        }
    }
}
