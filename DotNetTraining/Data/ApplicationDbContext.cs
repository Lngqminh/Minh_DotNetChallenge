using Microsoft.EntityFrameworkCore;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
