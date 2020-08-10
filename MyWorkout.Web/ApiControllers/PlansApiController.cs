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
    public class PlansApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlansApiController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        // GET: api/PlansApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlans()
        {
            var plan = await _unitOfWork.PlanRepository
               .ReadAll()
               .ToListAsync();
            plan.ForEach(x => _unitOfWork.Load(x, x => (IEnumerable<WorkoutDay>)x.WorkoutDays));

            return plan;
        }
        //Details
        // GET: api/PlansApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            var plan = await _unitOfWork.PlanRepository.Read(id);

            if (plan == null)
            {
                return NotFound();
            }
            _unitOfWork.Load(plan, x => (IEnumerable<WorkoutDay>)x.WorkoutDays);

            return plan;
        }

        // PUT: api/PlansApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.PlanRepository.Update(plan);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (!await PlanExists(id))
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

        // POST: api/PlansApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(Plan plan)
        {
            await _unitOfWork.PlanRepository.Create(plan);
            await _unitOfWork.Save();

            return CreatedAtAction("GetWorkoutDay", new { id = plan.Id }, plan);
        }

        // DELETE: api/PlansApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plan>> DeletePlan(int id)
        {
           if(await PlanExists(id)) // vot tut on rabotaet s vosklicatelnim znakom  esli budet pervaya strochka, a esli eye net to tak xocet ;
           {
                return NotFound(); 
           }

            await _unitOfWork.PlanRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private Task <bool> PlanExists(int id)
        {
            return _unitOfWork.RepeatRepository.IsExist(e => e.Id == id); 
        }
    }
}
