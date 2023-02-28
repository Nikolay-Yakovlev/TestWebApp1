using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>(b => b.ToTable("Department"));
            modelBuilder.Entity<Employee>(b =>
            { b.ToTable("Empoyee"); // в базе данных опечатка и используется не int для поля ID
                b.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric(5,0)");
            });
        }
    }
}
