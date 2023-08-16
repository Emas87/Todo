using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host=localhost:5432;Database=Todo;Username=postgres;Password={Environment.GetEnvironmentVariable("POSTGRESPSWD")};");

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
