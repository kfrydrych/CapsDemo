using CapsDemo.ServiceOne.Employees;
using CapsDemo.ServiceOne.Users;
using Microsoft.EntityFrameworkCore;

namespace CapsDemo.ServiceOne.Shared
{
    public interface IRepositories
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<User> Users { get; set; }
    }

    public class AppDbContext : DbContext, IRepositories
    {
        public const string ConnectionString = "data source=.; initial catalog=CapTests; integrated security=SSPI";

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}