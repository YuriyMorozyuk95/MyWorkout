using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Web.Data;
using MyWorkout.Web.Data.Entity;
using EF = Microsoft.EntityFrameworkCore;

namespace MyWorkout.Web.Controllers
{
    using Data.Repositories;
    using System.Collections.Generic;

    public class PlansController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlansController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var plan = await _unitOfWork.PlanRepository
                .ReadAll()
                .ToListAsync();
            return View(plan);
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plans = await _unitOfWork.PlanRepository
               .Read(id.Value);
            _unitOfWork.Load(plans, e => (IEnumerable<WorkoutDay>)e.WorkoutDays);

            if (plans == null)
            {
                return NotFound();
            }

            return View(plans);
        }
        
        // GET: Plans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.PlanRepository.Create(plan);
                await _unitOfWork.Save();
               return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _unitOfWork.PlanRepository
                .Read(id.Value);

            if (plan == null)
            { 
                return NotFound();
            }
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Plan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.PlanRepository.Update(plan);
                    await _unitOfWork.Save();
                }
                catch (EF.DbUpdateConcurrencyException)
                {
                    if (! await PlanExists(plan.Id))
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
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _unitOfWork.PlanRepository
                .Read(id.Value);

            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.PlanRepository.Delete(id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private  Task<bool> PlanExists(int id)
        {
            return _unitOfWork.PlanRepository.IsExist(e => e.Id == id);
        }
    }
}
