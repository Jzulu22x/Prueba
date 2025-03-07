using nombre.Models;
using Microsoft.EntityFrameworkCore;
namespace nombre.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Job> Jobs { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}