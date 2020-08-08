using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Web.Data.Entity;
using MyWorkout.Web.Data.Repositories;

namespace MyWorkout.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepeatsApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RepeatsApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Index
        // GET: api/RepeatsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repeat>>> GetRepeats()
        {
            var repeats = await _unitOfWork.RepeatRepository
                .ReadAll()
                .ToListAsync();

            return repeats;
        }

        //Detail
        // GET: api/RepeatsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repeat>> GetRepeat(int id)
        {
            var repeat = await _unitOfWork.RepeatRepository.Read(id);

            if (repeat == null)
            {
                return NotFound();
            }

            return repeat;
        }


        //Edit
        // PUT: api/RepeatsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepeat(int id, Repeat repeat)
        {
            if (id != repeat.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.RepeatRepository.Update(repeat);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (!await RepeatExists(id))
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
        // POST: api/RepeatsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Repeat>> PostRepeat(Repeat repeat)
        {
           await _unitOfWork.RepeatRepository.Create(repeat);
           await _unitOfWork.Save();

           return CreatedAtAction("GetRepeat", new { id = repeat.Id }, repeat);
        }

        // DELETE: api/RepeatsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Repeat>> DeleteRepeat(int id)
        {
            if (!await RepeatExists(id))
            {
                return NotFound();
            }

            await _unitOfWork.RepeatRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private Task<bool> RepeatExists(int id)
        {
            return _unitOfWork.RepeatRepository.IsExist(e => e.Id == id);
        }
    }
}
