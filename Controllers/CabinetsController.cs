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
    public class CabinetsController(AppDbContext context) : ControllerBase
    {

        // GET: api/Cabinets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cabinet>>> GetCabinets()
        {
            return await context.Cabinets.ToListAsync();
        }

        // GET: api/Cabinets/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpGet("{id}")]
        public async Task<ActionResult<Cabinet>> GetCabinet(Guid id)
        {
            var cabinet = await context.Cabinets.FindAsync(id);

            if (cabinet == null)
            {
                return NotFound();
            }

            return cabinet;
        }

        // PUT: api/Cabinets/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabinet(Guid id, CabinetRequest request)
        {
            var cabinet = new Cabinet(id, request.Number);

            context.Entry(cabinet).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabinetExists(id))
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

        // POST: api/Cabinets
        [HttpPost]
        public async Task<ActionResult<Cabinet>> PostCabinet(CabinetRequest request)
        {
            var cabinet = new Cabinet(Guid.NewGuid(), request.Number);

            context.Cabinets.Add(cabinet);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCabinet", new { id = cabinet.Id }, cabinet);
        }

        // DELETE: api/Cabinets/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabinet(Guid id)
        {
            var cabinet = await context.Cabinets.FindAsync(id);
            if (cabinet == null)
            {
                return NotFound();
            }

            context.Cabinets.Remove(cabinet);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CabinetExists(Guid id)
        {
            return context.Cabinets.Any(e => e.Id == id);
        }
    }
}
