using _17may.Models;
using Microsoft.EntityFrameworkCore;

namespace _17may.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Doctors> Doctors { get; set; }
    }
}
