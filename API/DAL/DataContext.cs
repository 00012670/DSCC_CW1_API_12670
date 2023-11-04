using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> o) : base(o)
        {
            Database.EnsureCreated();
        }
        public DbSet<Habit> Habits { get; set; }
    }
}
