﻿using System;
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
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            return await _context.Regions.ToListAsync();
        }

        // GET: api/Regions/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(Guid id)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(Guid id, RegionRequest request)
        {
            var region = new Region(id, request.Number);

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regions
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(RegionRequest request)
        {
            var region = new Region(Guid.NewGuid(), request.Number);
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.Id }, region);
        }

        // DELETE: api/Regions/5e6d4e53-6aac-4d0e-9db8-57b89359b72b
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionExists(Guid id)
        {
            return _context.Regions.Any(e => e.Id == id);
        }
    }
}
