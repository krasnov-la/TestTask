using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTasks.Contracts.Doctor;
using TestTasks.DataAccess;
using TestTasks.Domain;

namespace TestTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(AppDbContext context) : ControllerBase
    {

        // GET: api/Doctors
        [HttpGet]
        [Produces(typeof(List<DoctorFullResponse>))]
        public async Task<IActionResult> GetDoctors()
        {
            return Ok((await context
                .Doctors
                .Include(d => d.Cabinet)
                .Include(d => d.Specialization)
                .Include(d => d.Region)
                .ToListAsync())
                .Select(ToFullResponse));
        }

        // GET: api/Doctors/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpGet("{id}")]
        [Produces(typeof(DoctorResponse))]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            var doctor = await context
                .Doctors
                .FirstAsync(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(doctor));
        }

        // PUT: api/Doctors/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(Guid id, DoctorRequest request)
        {
            var doctor = new Doctor(
                request.FullName,
                request.CabinetId,
                request.SpecializationId,
                request.RegionId)
            { 
                Id = id
            };


            context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        [HttpPost]
        [Produces(typeof(DoctorResponse))]

        public async Task<ActionResult<Doctor>> PostDoctor(DoctorRequest request)
        {
            var doctor = new Doctor(
                request.FullName,
                request.CabinetId,
                request.SpecializationId,
                request.RegionId);

            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, ToResponse(doctor));
        }

        // DELETE: api/Doctors/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(Guid id)
        {
            return context.Doctors.Any(e => e.Id == id);
        }

        private static DoctorFullResponse ToFullResponse(Doctor doctor)
        {
            return new DoctorFullResponse(
                    Id: doctor.Id,
                    FullName: doctor.FullName,
                    Cabinet: doctor.Cabinet,
                    Specialization: doctor.Specialization,
                    Region: doctor.Region
                );
        }

        private static DoctorResponse ToResponse(Doctor doctor)
        {
            return new DoctorResponse(
                doctor.Id,
                doctor.FullName,
                doctor.CabinetId,
                doctor.SpecializationId,
                doctor.RegionId);
        }
    }
}
