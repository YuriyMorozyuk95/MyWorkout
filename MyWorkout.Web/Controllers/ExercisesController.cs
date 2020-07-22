using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Web.Data.Entity;

namespace MyWorkout.Web.Controllers
{
    using Data.Repositories;

    public class ExercisesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExercisesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Exercises
        //Exersices/CreateFromDayOfWeek/1

        public IActionResult CreateFromDayOfWeek(int? id)
        {
            if (id == default(int))
            {
                throw new ArgumentNullException("Day of week id is not set");
            }

            var exercise = new Exercise()
            {
                WorkoutDayId = id.Value
            };

            return View(nameof(Create), exercise);
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.ExerciseRepository.ReadAll().ToListAsync();

            return View(data);
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _unitOfWork.ExerciseRepository
                .Read(id.Value);
            _unitOfWork.Load(exercise, e => (IEnumerable<Repeat>)e.Repeats);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Name,RestTime,WorkoutDayId")] Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            await _unitOfWork.ExerciseRepository
                .Create(exercise);

            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _unitOfWork.ExerciseRepository
                .Read(id.Value);

            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RestTime,WorkoutDayId")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(exercise);
            }
            try
            {
                _unitOfWork.ExerciseRepository
                    .Update(exercise);

                await _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                var isExist = await ExerciseExists(exercise.Id);

                if (!isExist)
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _unitOfWork.ExerciseRepository
                .Read(id.Value);


            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int workoutDayId)
        {
            await _unitOfWork.ExerciseRepository.Delete(id);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private Task<bool> ExerciseExists(int id)
        {
            return _unitOfWork.ExerciseRepository
                .ReadAll()
                .AnyAsync(e => e.Id == id)
                .AsTask();
        }
    }
}
