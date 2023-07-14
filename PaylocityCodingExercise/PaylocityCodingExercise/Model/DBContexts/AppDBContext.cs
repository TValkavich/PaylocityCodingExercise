using Microsoft.EntityFrameworkCore;
using PaylocityCodingExercise.Model.Models;

namespace PaylocityCodingExercise.Model.DBContexts
{
    public class AppDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Model/Data/AppDB.db");
        }
    }
}
