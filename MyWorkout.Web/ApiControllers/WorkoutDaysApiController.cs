using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using MyWorkout.Web.Data.Entity;
using MyWorkout.Web.Data.Repositories;

namespace MyWorkout.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutDaysApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutDaysApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //index
        // GET: api/WorkoutDaysApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDay>>> GetWorkoutDays()
        {
            var workoutDays = await _unitOfWork.WorkoutDayRepository
                .ReadAll()
                .ToListAsync();
            workoutDays.ForEach(x => _unitOfWork.Load(x, x => (IEnumerable<Exercise>)x.Exercises));

            return workoutDays;
        }
        //Details
        // GET: api/WorkoutDaysApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDay>> GetWorkoutDay(int id)
        {
            var workoutDay = await _unitOfWork.WorkoutDayRepository.Read(id);
            _unitOfWork.Load(workoutDay, x => (IEnumerable<Exercise>)x.Exercises);

            if (workoutDay == null)
            {
                return NotFound();
            }

            return workoutDay;
        }
        //Edit
        // PUT: api/WorkoutDaysApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutDay(int id, WorkoutDay workoutDay)
        {
            if (id != workoutDay.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.WorkoutDayRepository.Update(workoutDay);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (!await WorkoutDayExists(id))
                {
                    return NotFound(ex);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Create
        // POST: api/WorkoutDaysApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkoutDay>> PostWorkoutDay(WorkoutDay workoutDay)
        {
            await _unitOfWork.WorkoutDayRepository.Create(workoutDay);
            await _unitOfWork.Save();

            return CreatedAtAction("GetWorkoutDay", new { id = workoutDay.Id }, workoutDay);
        }

        // DELETE: api/WorkoutDaysApi/5
        [HttpDelete("{id}")]
       public async Task<ActionResult<WorkoutDay>> DeleteWorkoutDay(int id)
        {
           var workoutDay = await _unitOfWork.WorkoutDayRepository.Read(id);
            await WorkoutDayExists(id);
          
           
            await _unitOfWork.WorkoutDayRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private Task <bool> WorkoutDayExists(int id)
        {
            return _unitOfWork.WorkoutDayRepository.IsExist(e => e.Id == id);
        }
    }
}
