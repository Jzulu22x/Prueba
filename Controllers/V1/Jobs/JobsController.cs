using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nombre.Data;
using nombre.DTOs;
using nombre.Models;

namespace nombre.Controllers.V1.Jobs;

[ApiController]
[Route("api/v1/jobs")]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task <ActionResult<IEnumerable<Job>>> GetAll()
    {
        return await _context.Jobs.ToListAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Job>> GetById(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job == null)
        {
            return NotFound();
        }

        return job;
    }

    [HttpPost]
    public async Task<ActionResult<Job>> Create(JobDTO jobDTO)
    {
        var newJob = new Job { Name = jobDTO.Name, Description = jobDTO.Description, Completed = false};
        if (await _context.Jobs.AnyAsync(j => j.Name == jobDTO.Name))
        {
            return Conflict("Error, la tarea ya existe.");
        }
        _context.Jobs.Add(newJob);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = newJob.id }, newJob);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Completed(int id)
    {
        var jobs = await _context.Jobs.FindAsync(id);

        if (jobs == null)
        {
            return NotFound();
        }

        jobs.Completed = true;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        var jobs = await _context.Jobs.FindAsync(id);
        if (jobs == null)
        {
            return NotFound();
        }
        _context.Jobs.Remove(jobs);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IEnumerable<Job>>> GetCompleted()
    {
        return await _context.Jobs.Where(j => j.Completed == true).ToListAsync();
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<Job>>> Jobpending()
    {
        return await _context.Jobs.Where(j => j.Completed == false).ToListAsync();
    }
}
