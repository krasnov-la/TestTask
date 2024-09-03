using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTasks.Contracts;
using TestTasks.DataAccess;
using TestTasks.Domain;

namespace TestTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SpecializationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Specializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            return await _context.Specializations.ToListAsync();
        }

        // GET: api/Specializations/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpecialization(Guid id)
        {
            var specialization = await _context.Specializations.FindAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }

            return specialization;
        }

        // PUT: api/Specializations/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(Guid id, SpecializationRequest request)
        {
            var specialization = new Specialization(id, request.Name);
            if (id != specialization.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Specializations
        [HttpPost]
        public async Task<ActionResult<Specialization>> PostSpecialization(SpecializationRequest request)
        {
            var specialization = new Specialization(Guid.NewGuid(), request.Name);
            _context.Specializations.Add(specialization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialization", new { id = specialization.Id }, specialization);
        }

        // DELETE: api/Specializations/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(Guid id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _context.Specializations.Remove(specialization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecializationExists(Guid id)
        {
            return _context.Specializations.Any(e => e.Id == id);
        }
    }
}
