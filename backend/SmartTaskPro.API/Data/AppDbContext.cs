using Microsoft.EntityFrameworkCore;
using SmartTaskPro.API.Models;

namespace SmartTaskPro.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // These will become your database tables
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
