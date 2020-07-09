using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Web.Data;
using MyWorkout.Web.Data.Entity;
using EF = Microsoft.EntityFrameworkCore;

namespace MyWorkout.Web.Controllers
{
    using Data.Repositories;
    using System;
    using System.Collections.Generic;

    public class WorkoutDaysController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutDaysController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Repeats/CreateFromExercise/328
        public IActionResult CreateFromPlan(int? Id)
        {
            if (Id == default(int))
            {
                throw new ArgumentNullException("plan id is not set");
            }

            var repeat = new WorkoutDay()
            {
                PlanId = Id.Value
            };

            return View(nameof(Create), repeat);
        }

        // GET: WorkoutDays
        public async Task<IActionResult> Index()
        {
            var workoutDays = await _unitOfWork.WorkoutDayRepository
                .ReadAll()
                .ToListAsync();

            return View(workoutDays);
        }

        // GET: WorkoutDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutDay = await _unitOfWork.WorkoutDayRepository
                .Read(id.Value);
            _unitOfWork.Load(workoutDay, w => (IEnumerable<Exercise>)w.Exercises);

            if (workoutDay == null)
            {
                return NotFound();
            }

            return View(workoutDay);
        }

        // GET: WorkoutDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,MuscleGroup,PlanId")] WorkoutDay workoutDay)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.WorkoutDayRepository.Create(workoutDay);
                await _unitOfWork.Save();

                return RedirectToAction("Plans","Details",workoutDay);
            }
            return View(workoutDay);
        }

        // GET: WorkoutDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutDay = await _unitOfWork.WorkoutDayRepository
                .Read(id.Value);

            if (workoutDay == null)
            {
                return NotFound();
            }
            return View(workoutDay);
        }

        // POST: WorkoutDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,MuscleGroup, PlanId")] WorkoutDay workoutDay)
        {
            if (id != workoutDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.WorkoutDayRepository.Update(workoutDay);
                    await _unitOfWork.Save();
                }
                catch (EF.DbUpdateConcurrencyException)
                {
                    if (!await WorkoutDayExists(workoutDay.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction("Plans","Details",workoutDay.Id);
            }
            return View(workoutDay);
        }

        // GET: WorkoutDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutDay = await _unitOfWork.WorkoutDayRepository
                .Read(id.Value);

            if (workoutDay == null)
            {
                return NotFound();
            }

            return View(workoutDay);
        }

        // POST: WorkoutDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.WorkoutDayRepository.Delete(id);
            await _unitOfWork.Save();

            return RedirectToAction("Plans","Details",id);
        }

        private Task<bool> WorkoutDayExists(int id)
        {
            return _unitOfWork.WorkoutDayRepository.IsExist(e => e.Id == id);
        }
    }
}
