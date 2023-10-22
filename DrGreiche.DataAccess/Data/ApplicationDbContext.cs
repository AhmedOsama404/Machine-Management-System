using DrGreiche.Models;
using Microsoft.EntityFrameworkCore;

namespace DrGreiche.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Machine> machines { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<MachineLocation> machineLocation { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    } 
}
