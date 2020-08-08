using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Web.Data;
using MyWorkout.Web.Data.Entity;
using MyWorkout.Web.Data.Repositories;

namespace MyWorkout.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExercisesApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ExercisesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
        {
            var exercise = await _unitOfWork.ExerciseRepository
                .ReadAll()
                .ToListAsync();
            exercise.ForEach(x => _unitOfWork.Load(x, x => (IEnumerable<Repeat>)x.Repeats));

            return exercise;
        }
        //Detail
        // GET: api/ExercisesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(int id)
        {
            var exercise = await _unitOfWork.ExerciseRepository.Read(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return exercise;
        }
        //Edit
        // PUT: api/ExercisesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.ExerciseRepository.Update(exercise);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (!await ExerciseExists(id))
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
        // POST: api/ExercisesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            await _unitOfWork.ExerciseRepository.Create(exercise);
            await _unitOfWork.Save();

            return CreatedAtAction("GetRepeat", new { id = exercise.Id }, exercise);
        }

        // DELETE: api/ExercisesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exercise>> DeleteExercise(int id)
        {
            if (!await ExerciseExists(id))
            {
                return NotFound();
            }

            await _unitOfWork.ExerciseRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private Task <bool> ExerciseExists(int id)
        {
            return _unitOfWork.ExerciseRepository.IsExist(e => e.Id == id);
        }
    }
}
