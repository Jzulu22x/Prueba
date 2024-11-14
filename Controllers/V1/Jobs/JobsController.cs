using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nombre.Data;
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
}
