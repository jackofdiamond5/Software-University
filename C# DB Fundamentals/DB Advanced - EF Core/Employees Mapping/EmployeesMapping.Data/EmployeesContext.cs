using EmployeesMapping.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesMapping.Data
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext() { }

        public EmployeesContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configure.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsRequired();

                entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsRequired();

                entity.Property(e => e.Salary)
                .IsRequired();

                entity.Property(e => e.Birthday)
                .IsRequired(false);

                entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsRequired(false);
            });
        }
    }
}
