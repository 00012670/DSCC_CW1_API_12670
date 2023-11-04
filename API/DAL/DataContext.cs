using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class DataContext : DbContext
    {
        // Constructor that takes DbContextOptions as a parameter
        public DataContext(DbContextOptions<DataContext> o) : base(o)
        {
            // Ensuring that the database is created when the context is initialized
            Database.EnsureCreated();
        }

        // DbSet properties for the tables
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Progress> Progresses { get; set; }
    }
}
