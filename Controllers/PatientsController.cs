using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTasks.Contracts.Patient;
using TestTasks.DataAccess;
using TestTasks.Domain;

namespace TestTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(AppDbContext context) : ControllerBase
    {

        // GET: api/Patients
        [HttpGet]
        [Produces(typeof(List<PatientFullResponse>))]

        public async Task<IActionResult> GetPatients()
        {
            return Ok((await context
                .Patients
                .Include(p => p.Region)
                .ToListAsync())
                .Select(ToFullResponse));
        }

        // GET: api/Patients/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpGet("{id}")]
        [Produces(typeof(PatientResponse))]

        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(patient));
        }

        // PUT: api/Patients/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(Guid id, PatientRequest request)
        {
            var patient = new Patient(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address,
                request.DateOfBirth,
                (Sex)Enum.Parse(typeof(Sex), request.Sex),
                request.RegionId
                )
            { 
                Id = id
            };

            context.Entry(patient).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [HttpPost]
        [Produces(typeof(PatientResponse))]

        public async Task<IActionResult> PostPatient(PatientRequest request)
        {
            var patient = new Patient(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address,
                request.DateOfBirth,
                (Sex)Enum.Parse(typeof(Sex), request.Sex),
                request.RegionId
                );
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, ToResponse(patient));
        }

        // DELETE: api/Patients/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(Guid id)
        {
            return context.Patients.Any(e => e.Id == id);
        }

        private static PatientResponse ToResponse(Patient patient)
        {
            return new PatientResponse(
                Id: patient.Id,
                FirstName: patient.FirstName,
                MiddleName: patient.MiddleName,
                LastName: patient.LastName,
                Address: patient.Address,
                DateOfBirth: patient.BirthDate,
                Sex: patient.Sex.ToString(),
                RegionId: patient.RegionId
                );
        }

        private static PatientFullResponse ToFullResponse(Patient patient)
        {
            return new PatientFullResponse(
                Id: patient.Id,
                FirstName: patient.FirstName,
                MiddleName: patient.MiddleName,
                LastName: patient.LastName,
                Address: patient.Address,
                DateOfBirth: patient.BirthDate,
                Sex: patient.Sex.ToString(),
                Region: patient.Region
                );
        }
    }
}
